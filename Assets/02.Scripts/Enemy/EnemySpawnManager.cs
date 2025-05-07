using Redcode.Pools;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Vector3 spawnAreaMin; // Minimum bounds of the spawn area
    [SerializeField] private Vector3 spawnAreaMax; // Maximum bounds of the spawn area

    private float spawnTimer = 0f; // Timer to track time elapsed
    [SerializeField] private float spawnInterval = 2f; // Interval in seconds between spawns

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // Reset the timer
        }
    }

    private void SpawnEnemy()
    {
        // Get a random position within the spawn area
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // 10% chance to spawn an Elite enemy
        //if (Random.value <= 0.1f)
        //{
        //    var eliteEnemy = PoolManager.Instance.GetFromPool<EliteEnemy>();
        //    eliteEnemy.transform.position = spawnPosition;
        //}
        //else
        //{
        //    var enemy = PoolManager.Instance.GetFromPool<Enemy>();
        //    enemy.transform.position = spawnPosition;
        //}

        var enemy = PoolManager.Instance.GetFromPool<Enemy>();
        enemy.transform.position = spawnPosition;
    }

    // Generates a random position within the specified spawn range
    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        float z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
        return new Vector3(x, y, z);
    }

    // Visualize the spawn area in the Scene view using Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 center = (spawnAreaMin + spawnAreaMax) / 2;
        Vector3 size = spawnAreaMax - spawnAreaMin;
        Gizmos.DrawCube(center, size);
    }
}
