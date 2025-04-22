using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private PlayerStatsSO stats;
    [SerializeField]
    private float throwForce;

    public GameObject FirePos;
    public GameObject BombPrefab;
    public ParticleSystem BulletEffect;

    private bool isReloading = false;
    private int bulletCount;
    private int bombCount;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        bulletCount = stats.MaxBullets;
        bombCount = stats.MaxBombs;
        PlayerUI.Instance.SetBulletCount(bulletCount, stats.MaxBullets);
        PlayerUI.Instance.SetBombCount(bombCount, stats.MaxBombs);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        //탄
        if (Input.GetMouseButtonDown(0))
        {
            if(bulletCount <= 0 || isReloading)
            {
                Debug.Log("No Bullets Left");
                return;
            }

            Ray ray = new Ray(FirePos.transform.position, FirePos.transform.forward);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo = new RaycastHit();
            bool isHit = Physics.Raycast(ray, out hitInfo);

            if (isHit)
            {
                BulletEffect.transform.position = hitInfo.point;
                BulletEffect.transform.forward = hitInfo.normal;
                BulletEffect.Play();

                //Instantiate(BulletEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

                Debug.Log("Hit: " + hitInfo.collider.name);
            }
            else
            {
                Debug.Log("Miss");
            }
            bulletCount--;
            PlayerUI.Instance.SetBulletCount(bulletCount, stats.MaxBullets);
        }

        //폭
        if (Input.GetMouseButtonDown(1))
        {
            if(bombCount <= 0 || isReloading)
            {
                Debug.Log("No Bombs Left");
                return;
            }

            GameObject bomb = Instantiate(BombPrefab, FirePos.transform.position, FirePos.transform.rotation);
            Rigidbody bombRigid = bomb.GetComponent<Rigidbody>();

            bombRigid.AddForce(FirePos.transform.forward * throwForce, ForceMode.Impulse);
            bombRigid.AddTorque(Vector3.one);
            bombCount--;
            PlayerUI.Instance.SetBombCount(bombCount, stats.MaxBombs);
        }

        if(Input.GetMouseButtonUp(1))
        {

        }

    }

    private IEnumerator Reload()
    {
        if (isReloading)
            yield break;
        isReloading = true;
        Debug.Log("Reloading...");
        StartCoroutine(PlayerUI.Instance.ReloadProgress(stats.ReloadTime));
        yield return new WaitForSeconds(stats.ReloadTime);
        bulletCount = stats.MaxBullets;
        bombCount = stats.MaxBombs;
        PlayerUI.Instance.SetBulletCount(bulletCount, stats.MaxBullets);
        PlayerUI.Instance.SetBombCount(bombCount, stats.MaxBombs);
        isReloading = false;
    }

    private void OnDrawGizmos()
    {
        if (FirePos != null)
        {
            // Gizmo 색상 설정
            Gizmos.color = Color.red;

            // 쏘는 방향으로 선 그리기
            Gizmos.DrawLine(FirePos.transform.position, FirePos.transform.position + FirePos.transform.forward * 5f);

            // 시작 지점에 구체 그리기
            Gizmos.DrawSphere(FirePos.transform.position, 0.1f);
        }
    }
}
