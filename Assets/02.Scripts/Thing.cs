using Redcode.Pools;
using UnityEditor.EditorTools;
using UnityEngine;
using System.Collections;

public class Thing : MonoBehaviour, IPoolObject
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private int explosionDamage;
    [SerializeField]
    private float flyAwayDuration;
    [SerializeField]
    private float flyAwaySpeed;

    public void TakeDamage(Damage damage)
    {
        health -= damage.Value;
        if (health <= 0)
        {
            Die();
            health = 0;
        }
   
    }

    public void Die()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            var damageable = hitCollider.GetComponent<Thing>();
            if (damageable != null && damageable != this && damageable.health > 0)
            {
                Damage damage = new Damage
                {
                    Value = explosionDamage,
                    From = this.gameObject
                };
                damageable.TakeDamage(damage);
            }
        }

        var explosionEffect = PoolManager.Instance.GetFromPool<ExplosionEffect>();
        explosionEffect.transform.position = transform.position;
        explosionEffect.transform.rotation = Quaternion.identity;

        StartCoroutine(FlyAwayAndDestroy());
    }

    private IEnumerator FlyAwayAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 flyDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;

        while (elapsedTime < flyAwayDuration)
        {
            transform.position += flyDirection * flyAwaySpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    public void OnCreatedInPool()
    {
        
    }

    public void OnGettingFromPool()
    {
        gameObject.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
