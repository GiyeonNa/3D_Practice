using UnityEngine;

public class AttackState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly EnemyFsm _fsm;
    private float _attackCurrentTime;

    public AttackState(Enemy enemy, EnemyFsm fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }

    public void Enter()
    {
        _attackCurrentTime = 0f;
    }

    public void Execute()
    {
        if (Vector3.Distance(_enemy.transform.position, _enemy.player.transform.position) >= _enemy.AttackDistance)
        {
            _fsm.ChangeState(eEnemyState.Trace);
            return;
        }

        if (_attackCurrentTime <= 0)
        {
            _attackCurrentTime = _enemy.AttackDelayTime;
        }
        else
        {
            _attackCurrentTime -= Time.deltaTime;
        }
    }

    public void Exit()
    {
        // Logic for exiting Attack state
    }
}
