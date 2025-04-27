using UnityEngine;

public class IdleState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly EnemyFsm _fsm;
    private float _patrolCurrentTime;

    public IdleState(Enemy enemy, EnemyFsm fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }

    public void Enter()
    {
        _patrolCurrentTime = 0f;
    }

    public void Execute()
    {
        _patrolCurrentTime += Time.deltaTime;

        if (Vector3.Distance(_enemy.transform.position, _enemy.player.transform.position) < _enemy.FindDistance)
        {
            _fsm.ChangeState(eEnemyState.Trace);
        }
        else if (_patrolCurrentTime >= _enemy.PatrolChangeTime)
        {
            _fsm.ChangeState(eEnemyState.Patrol);
        }
    }

    public void Exit()
    {
        // Logic for exiting Idle state
    }
}
