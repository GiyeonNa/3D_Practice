using UnityEngine;

public class AttackState : IEnemyState
{
    private readonly Enemy enemy;
    private readonly EnemyFsm fsm;
    private float attackCurrentTime;

    public AttackState(Enemy enemy, EnemyFsm fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public void Enter()
    {
        enemy.animator.SetTrigger("Attack");
        attackCurrentTime = 0f;
    }

    public void Execute()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) >= enemy.AttackDistance)
        {
            fsm.ChangeState(eEnemyState.Trace);
            return;
        }

        if (attackCurrentTime <= 0)
        {
            attackCurrentTime = enemy.AttackDelayTime;
        }
        else
        {
            attackCurrentTime -= Time.deltaTime;
        }
    }

    public void Exit()
    {
        // Logic for exiting Attack state
    }
}
