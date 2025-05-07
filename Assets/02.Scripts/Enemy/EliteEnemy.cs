using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EliteEnemy : Enemy
{
    [Header("Elite Enemy Settings")]
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private int explosionDamage = 50;
    [SerializeField] private GameObject explosionEffectPrefab;

    protected override void InitializeEnemy()
    {
        base.InitializeEnemy();
        enemyFSM = new EliteEnemyFSM(this);
    }

    public override void TakeDamage(Damage damage)
    {
        base.TakeDamage(damage);
    }

    public void TriggerExplosion()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<IDamageable>(out var damageable))
            {
                Damage damage = new Damage { Value = explosionDamage, From = this.gameObject };
                damageable.TakeDamage(damage);
            }
        }
    }
}

