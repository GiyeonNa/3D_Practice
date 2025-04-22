using Redcode.Pools;
using UnityEngine;

public class Bomb : MonoBehaviour, IPoolObject
{
    private PoolManager _poolManager;

    private void Awake()
    {
        _poolManager = Object.FindFirstObjectByType<PoolManager>();
    }


    public void OnCreatedInPool()
    {
    }

    public void OnGettingFromPool()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_poolManager != null)
        {
            var explosionEffect = _poolManager.GetFromPool<ExplosionEffect>();
            explosionEffect.transform.position = transform.position;
            explosionEffect.transform.rotation = Quaternion.identity;
        }

        if (_poolManager != null)
            _poolManager.TakeToPool<Bomb>(this);
        else
            Destroy(gameObject);
    }
}
