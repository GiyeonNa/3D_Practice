using System.Collections;
using UnityEngine;
using Redcode.Pools;
using NUnit.Framework.Constraints;
using UnityEditor.Experimental.GraphView;

public class PlayerFire : MonoBehaviour
{
    public static PlayerFire Instance { get; private set; }
    public GameObject FirePos;

    private PoolManager poolManager;
    private bool isReloading = false;
    private float currentThrowForce;
    private float currentFirerate;
    private bool isUIActive = false;
    private int currentAmmoCount;

    [SerializeField]
    private float angle;
    [SerializeField]
    private float range;
    int segments = 20;


    [SerializeField]
    private Animator animator;

    private bool isZoomMode;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Cursor.lockState = CursorLockMode.Confined;
        poolManager = Thing.FindFirstObjectByType<PoolManager>();
    }

    private void Start()
    {
        currentThrowForce = 0f;
        currentFirerate = 0f;

        var weaponInfo = PlayerManager.Instance.GetWeaponInfo();
        currentAmmoCount = weaponInfo.currentAmmo; // Use current ammo from weapon
        PlayerUI.Instance.SetAmmoCount(currentAmmoCount, weaponInfo.MaxAmmo);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isUIActive = !isUIActive;
            Cursor.lockState = isUIActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isUIActive;
        }

        if (isUIActive)
            return;

        currentFirerate += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());

        switch(PlayerManager.Instance.CurrentWeapon.Type)
        {
            case WeaponType.Gun:
                HandleGunFire();
                break;

            case WeaponType.Knife:
                HandleKnifeAttack();
                break;

            case WeaponType.Boom:
                HandleBoom();
                break;
        }
    }

    private void HandleBoom()
    {
        if (Input.GetMouseButton(1))
        {
            if (currentAmmoCount <= 0 || isReloading)
                return;

            currentThrowForce += Time.deltaTime * PlayerManager.Instance.GetWeaponInfo().MaxThrowForce;
            currentThrowForce = Mathf.Clamp(currentThrowForce, 0, PlayerManager.Instance.GetWeaponInfo().MaxThrowForce);
            PlayerUI.Instance.UpdateThrowForceUI(currentThrowForce, PlayerManager.Instance.GetWeaponInfo().MaxThrowForce);
        }

        if (Input.GetMouseButtonUp(1))
        {

            if (currentAmmoCount <= 0 || isReloading)
                return;

            if (poolManager != null)
            {
                var bomb = poolManager.GetFromPool<Bomb>();
                bomb.transform.position = FirePos.transform.position;
                bomb.transform.rotation = FirePos.transform.rotation;

                var bombRigid = bomb.GetComponent<Rigidbody>();
                bombRigid.linearVelocity = Vector3.zero;
                bombRigid.angularVelocity = Vector3.zero;
                bombRigid.AddForce(FirePos.transform.forward * currentThrowForce, ForceMode.Impulse);
                bombRigid.AddTorque(Vector3.one);
            }

            currentAmmoCount--;
            PlayerManager.Instance.GetWeaponInfo().currentAmmo = currentAmmoCount; // Update weapon's ammo
            currentThrowForce = 0f;
            PlayerUI.Instance.SetAmmoCount(currentAmmoCount, PlayerManager.Instance.GetWeaponInfo().MaxAmmo);
            PlayerUI.Instance.SetFalseThrowForceUI();
        }
    }

    private void HandleGunFire()
    {
        if (Input.GetMouseButton(0))
        {
            if (currentAmmoCount <= 0 || isReloading)
                return;

            if (currentFirerate < PlayerManager.Instance.GetWeaponInfo().FireRate)
                return;

            Ray ray = new Ray(FirePos.transform.position, FirePos.transform.forward);
            RaycastHit hitInfo = new RaycastHit();
            bool isHit = Physics.Raycast(ray, out hitInfo);

            if (hitInfo.collider == null)
                return;

            if (hitInfo.collider.CompareTag("Player"))
                return;

            if (isHit)
            {
                var bulletEffect = poolManager.GetFromPool<BulletEffect>();
                bulletEffect.transform.position = hitInfo.point;
                bulletEffect.transform.forward = hitInfo.normal;

                Debug.Log("Hit: " + hitInfo.collider.name);

                if(hitInfo.collider.GetComponent<IDamageable>() != null)
                {
                    Damage damage = new Damage();
                    damage.Value = 20;
                    damage.From = this.gameObject;
                    hitInfo.collider.GetComponent<IDamageable>().TakeDamage(damage);
                }
            }
            else
            {
                Debug.Log("Miss");
            }
            currentAmmoCount--;
            currentFirerate = 0f;
            animator.SetTrigger("Shoot");
            PlayerUI.Instance.SetAmmoCount(currentAmmoCount, PlayerManager.Instance.GetWeaponInfo().MaxAmmo);
        }


        //if (Input.GetMouseButton(1))
        //{
        //    isZoomMode = !isZoomMode;
        //    PlayerUI.Instance.ZoomImage(isZoomMode);
        //}


    }

    private void HandleKnifeAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Retrieve the name of the currently playing animation
            var currentStateInfo = animator.GetCurrentAnimatorStateInfo(2);
            if (currentStateInfo.IsName("Slash") && currentStateInfo.normalizedTime < 1.0f)
            {
                return; // Return if the 'Slash' animation is already playing
            }

            // Logic for knife attack
            animator.SetTrigger("Slash");
            Collider[] hitColliders = Physics.OverlapSphere(FirePos.transform.position, range);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject == this.gameObject)
                    continue;

                Vector3 directionToTarget = (hitCollider.transform.position - FirePos.transform.position).normalized;
                float angleToTarget = Vector3.Angle(FirePos.transform.forward, directionToTarget);

                if (angleToTarget <= angle / 2)
                {
                    if (hitCollider.TryGetComponent<IDamageable>(out var damageable))
                    {
                        Damage damage = new Damage();
                        damage.Value = 20;
                        damage.From = this.gameObject;
                        damageable.TakeDamage(damage);
                    }
                }
            }
        }
    }

    private IEnumerator Reload()
    {
        if (isReloading)
            yield break;

        isReloading = true;
        Debug.Log("Reloading...");
        StartCoroutine(PlayerUI.Instance.ReloadProgress(PlayerManager.Instance.GetWeaponInfo().ReloadTime));

        yield return new WaitForSeconds(PlayerManager.Instance.GetWeaponInfo().ReloadTime);

        currentAmmoCount = PlayerManager.Instance.GetWeaponInfo().MaxAmmo;
        PlayerManager.Instance.GetWeaponInfo().currentAmmo = currentAmmoCount; // Update weapon's ammo
        PlayerUI.Instance.SetAmmoCount(currentAmmoCount, PlayerManager.Instance.GetWeaponInfo().MaxAmmo);
        isReloading = false;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmoCount; 
    }

    public void UpdateAmmoCount(int ammo, int maxAmmo)
    {
        currentAmmoCount = ammo;
        PlayerUI.Instance.SetAmmoCount(currentAmmoCount, maxAmmo);
    }

    private void OnDrawGizmos()
    {
        if (FirePos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(FirePos.transform.position, FirePos.transform.position + FirePos.transform.forward * 5f);
            Gizmos.DrawSphere(FirePos.transform.position, 0.1f);

            Gizmos.color = Color.green;
            Vector3 startDirection = Quaternion.Euler(0, -angle / 2, 0) * FirePos.transform.forward;
            Vector3 previousPoint = FirePos.transform.position + startDirection * range;

            for (int i = 1; i <= segments; i++)
            {
                float currentAngle = -angle / 2 + (angle / segments) * i;
                Vector3 currentDirection = Quaternion.Euler(0, currentAngle, 0) * FirePos.transform.forward;
                Vector3 currentPoint = FirePos.transform.position + currentDirection * range;

                Gizmos.DrawLine(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }

            // 부채꼴의 시작과 끝을 FirePos와 연결
            Gizmos.DrawLine(FirePos.transform.position, FirePos.transform.position + startDirection * range);
            Gizmos.DrawLine(FirePos.transform.position, previousPoint);
        }
    }
}
