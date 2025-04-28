using UnityEngine;

public class IdleState : IEnemyState
{
    private readonly Enemy enemy;
    private readonly EnemyFsm fsm;
    private float patrolCurrentTime;

    public IdleState(Enemy enemy, EnemyFsm fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public void Enter()
    {
        patrolCurrentTime = 0f;
        enemy.animator.SetTrigger("MoveToIdle");
    }

    public void Execute()
    {
        patrolCurrentTime += Time.deltaTime;

        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.FindDistance)
        {
            fsm.ChangeState(eEnemyState.Trace);
        }
        else if (patrolCurrentTime >= enemy.PatrolChangeTime)
        {
            fsm.ChangeState(eEnemyState.Patrol);
        }
    }

    public void Exit()
    {
        // Logic for exiting Idle state
    }
}
