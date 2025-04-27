using UnityEngine;

public class ReturnState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly EnemyFsm _fsm;

    public ReturnState(Enemy enemy, EnemyFsm fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }

    public void Enter()
    {
        // Logic for entering Return state
    }

    public void Execute()
    {
        if (Vector3.Distance(_enemy.transform.position, _enemy.startPos) <= 0.1f)
        {
            _enemy.transform.position = _enemy.startPos;
            _fsm.ChangeState(eEnemyState.Idle);
            return;
        }

        if (Vector3.Distance(_enemy.transform.position, _enemy.player.transform.position) < _enemy.FindDistance)
        {
            _fsm.ChangeState(eEnemyState.Trace);
            return;
        }

        Vector3 direction = _enemy.startPos - _enemy.transform.position;
        direction.Normalize();
        _enemy.GetComponent<CharacterController>().Move(direction * _enemy.MoveSpeedf * Time.deltaTime);
    }

    public void Exit()
    {
        // Logic for exiting Return state
    }
}
