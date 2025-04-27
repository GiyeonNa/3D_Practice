using UnityEngine;

public class TraceState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly EnemyFsm _fsm;

    public TraceState(Enemy enemy, EnemyFsm fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }

    public void Enter()
    {
        // Logic for entering Trace state
    }

    public void Execute()
    {
        if (Vector3.Distance(_enemy.transform.position, _enemy.player.transform.position) >= _enemy.ReturnDistance)
        {
            _fsm.ChangeState(eEnemyState.Return);
            return;
        }

        if (Vector3.Distance(_enemy.transform.position, _enemy.player.transform.position) < _enemy.AttackDistance)
        {
            _fsm.ChangeState(eEnemyState.Attack);
            return;
        }

        Vector3 direction = _enemy.player.transform.position - _enemy.transform.position;
        direction.Normalize();
        _enemy.GetComponent<CharacterController>().Move(direction * _enemy.MoveSpeedf * Time.deltaTime);
    }

    public void Exit()
    {
        // Logic for exiting Trace state
    }
}
