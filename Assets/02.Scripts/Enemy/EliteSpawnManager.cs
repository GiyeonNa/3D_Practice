using UnityEngine;

public class EliteSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject eliteEnemyPrefab;
    [SerializeField] private float spawnInterval = 30f;
    [SerializeField] private Vector3 spawnAreaMin;
    [SerializeField] private Vector3 spawnAreaMax;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEliteEnemy), spawnInterval, spawnInterval);
    }

    private void SpawnEliteEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        Instantiate(eliteEnemyPrefab, spawnPosition, Quaternion.identity);
    }
}
