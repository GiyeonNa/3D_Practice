using Redcode.Pools;
using UnityEditor.EditorTools;
using UnityEngine;

public class BulletEffect : MonoBehaviour, IPoolObject
{
    private PoolManager poolManager;
    private ParticleSystem particleSystem;

    private void Awake()
    {
        poolManager = Object.FindFirstObjectByType<PoolManager>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnCreatedInPool()
    {
       
    }
    public void OnGettingFromPool()
    {
        gameObject.SetActive(true);
        Invoke("ReturnToPool", 1f);
    }
    private void OnParticleSystemStopped()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (poolManager != null)
        {
            poolManager.TakeToPool<BulletEffect>(this);
        }
        else
        {
            Destroy(gameObject); // 풀 매니저가 없으면 파괴
        }
    }
}
