using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField]
    private int goldAmount;
    [SerializeField]
    private float absorptionRange = 5f; // ��� ����
    [SerializeField]
    private float absorptionSpeed = 5f; // ��� �ӵ�

    private Transform playerTransform;

    private void Start()
    {
        // �÷��̾��� Transform�� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
            playerTransform = player.transform;
    }

    private void Update()
    {
        if (playerTransform == null) 
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= absorptionRange)
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, absorptionSpeed * Time.deltaTime);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCurrencyManager.Instance.AddCurrency(goldAmount);
            Destroy(gameObject);
        }
    }
}
