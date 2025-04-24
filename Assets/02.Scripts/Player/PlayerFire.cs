using System.Collections;
using UnityEngine;
using Redcode.Pools;
using NUnit.Framework.Constraints;
using UnityEditor.Experimental.GraphView;

public class PlayerFire : MonoBehaviour
{
    public GameObject FirePos;

    private PoolManager poolManager;
    private bool isReloading = false;
    private int bulletCount;
    private int bombCount;
    private float currentThrowForce;
    private float currentFirerate;
    private bool isUIActive = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.lockState = CursorLockMode.Locked;
        poolManager = Thing.FindFirstObjectByType<PoolManager>();
    }

    private void Start()
    {
        bulletCount = PlayerManager.Instance.GetInfo().MaxBullets;
        bombCount = PlayerManager.Instance.GetInfo().MaxBombs;
        currentThrowForce = 0f;
        currentFirerate = 0f;
        PlayerUI.Instance.SetBulletCount(bulletCount, PlayerManager.Instance.GetInfo().MaxBullets);
        PlayerUI.Instance.SetBombCount(bombCount, PlayerManager.Instance.GetInfo().MaxBombs);
    }

    private void Update()
    {
        // Handle cursor visibility based on ESC key and UI interaction
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isUIActive = !isUIActive;
            Cursor.lockState = isUIActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isUIActive;
        }

        if (isUIActive)
            return;

        // Existing gameplay logic
        currentFirerate += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());

        if (Input.GetMouseButton(0))
        {
            if (bulletCount <= 0 || isReloading)
                return;

            if (currentFirerate < PlayerManager.Instance.GetInfo().FireRate)
                return;

            Ray ray = new Ray(FirePos.transform.position, FirePos.transform.forward);
            RaycastHit hitInfo = new RaycastHit();
            bool isHit = Physics.Raycast(ray, out hitInfo);

            if (hitInfo.collider.CompareTag("Player"))
                return;

            if (isHit)
            {
                var bulletEffect = poolManager.GetFromPool<BulletEffect>();
                bulletEffect.transform.position = hitInfo.point;
                bulletEffect.transform.forward = hitInfo.normal;

                Debug.Log("Hit: " + hitInfo.collider.name);

                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    var enemy = hitInfo.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        Damage damage = new Damage();
                        damage.Value = 20;
                        damage.From = this.gameObject;
                        enemy.TakeDamage(damage);
                    }
                }
                else if (hitInfo.collider.CompareTag("Thing"))
                {
                    var thing = hitInfo.collider.GetComponent<Thing>();
                    if (thing != null)
                    {
                        Damage damage = new Damage();
                        damage.Value = 20;
                        damage.From = this.gameObject;
                        thing.TakeDamage(damage);
                    }
                }
            }
            else
            {
                Debug.Log("Miss");
            }
            bulletCount--;
            currentFirerate = 0f;
            PlayerUI.Instance.SetBulletCount(bulletCount, PlayerManager.Instance.GetInfo().MaxBullets);
        }

        if (Input.GetMouseButton(1))
        {
            if (bombCount <= 0 || isReloading)
                return;

            currentThrowForce += Time.deltaTime * PlayerManager.Instance.GetInfo().MaxThrowForce;
            currentThrowForce = Mathf.Clamp(currentThrowForce, 0, PlayerManager.Instance.GetInfo().MaxThrowForce);
            PlayerUI.Instance.UpdateThrowForceUI(currentThrowForce, PlayerManager.Instance.GetInfo().MaxThrowForce);
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (bombCount <= 0 || isReloading)
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

            bombCount--;
            currentThrowForce = 0f;
            PlayerUI.Instance.SetBombCount(bombCount, PlayerManager.Instance.GetInfo().MaxBombs);
        }
    }

    private IEnumerator Reload()
    {
        if (isReloading)
            yield break;

        isReloading = true;
        Debug.Log("Reloading...");
        StartCoroutine(PlayerUI.Instance.ReloadProgress(PlayerManager.Instance.GetInfo().ReloadTime));

        yield return new WaitForSeconds(PlayerManager.Instance.GetInfo().ReloadTime);

        bulletCount = PlayerManager.Instance.GetInfo().MaxBullets;
        bombCount = PlayerManager.Instance.GetInfo().MaxBombs;
        PlayerUI.Instance.SetBulletCount(bulletCount, PlayerManager.Instance.GetInfo().MaxBullets);
        PlayerUI.Instance.SetBombCount(bombCount, PlayerManager.Instance.GetInfo().MaxBombs);
        isReloading = false;
    }

    private void OnDrawGizmos()
    {
        if (FirePos != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(FirePos.transform.position, FirePos.transform.position + FirePos.transform.forward * 5f);
            Gizmos.DrawSphere(FirePos.transform.position, 0.1f);
        }
    }
}
