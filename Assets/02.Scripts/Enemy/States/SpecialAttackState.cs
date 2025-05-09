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
            //PerformRangeAttack();
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
}
