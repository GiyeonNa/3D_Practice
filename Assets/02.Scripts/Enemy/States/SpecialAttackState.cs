using UnityEngine;

public class SpecialAttackState : IEnemyState
{
    private readonly Enemy enemy;
    private readonly EnemyFsm fsm;
    private bool attackPerformed;

    public SpecialAttackState(Enemy enemy, EnemyFsm fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public void Enter()
    {
        enemy.animator.SetTrigger("SpecialAttack");
        attackPerformed = false;
    }

    public void Execute()
    {
        if (!attackPerformed)
        {
            PerformRangeAttack();
            attackPerformed = true;
        }

        // Transition back to Idle or another state after the attack
        if (enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            fsm.ChangeState(eEnemyState.Idle);
        }
    }

    public void Exit()
    {
        // Logic for exiting SpecialAttack state
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
                Damage damage = new Damage { Value = 10, From = enemy.gameObject }; // Example damage calculation
                damageable.TakeDamage(damage);
            }
        }
    }
}
