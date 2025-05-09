using UnityEngine;

public class EliteSpecialAttackEvent : MonoBehaviour
{
    public EliteEnemy enemy;

    public void AttackEvent()
    {
        Debug.Log("EliteSpecialAttackEvent called");
        PerformRangeAttack();
    }

    private void PerformRangeAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, enemy.AttackDistance);
        foreach (var hitCollider in hitColliders)
        {
            // Exclude the enemy itself
            if (hitCollider.gameObject == enemy.gameObject)
            {
                continue;
            }

            if (hitCollider.TryGetComponent<IDamageable>(out var damageable))
            {
                Debug.Log("Hit " + hitCollider.name);
                Damage damage = new Damage { Value = 50, From = enemy.gameObject }; // Example damage calculation
                damageable.TakeDamage(damage);
            }
        }
    }
}
