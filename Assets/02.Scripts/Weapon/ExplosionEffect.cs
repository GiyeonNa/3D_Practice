using Redcode.Pools;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour, IPoolObject
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
    }
    private void OnParticleSystemStopped()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (poolManager != null)
            poolManager.TakeToPool<ExplosionEffect>(this);
        else
            Destroy(gameObject); 
    }
}
