using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly EnemyFsm _fsm;
    private Vector3? _currentTargetPos;

    public PatrolState(Enemy enemy, EnemyFsm fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }

    public void Enter()
    {
        _currentTargetPos = null;
    }

    public void Execute()
    {
        if (Vector3.Distance(_enemy.transform.position, _enemy.player.transform.position) < _enemy.FindDistance)
        {
            _fsm.ChangeState(eEnemyState.Trace);
            return;
        }

        if (_enemy.PatrolPosList.Count > 0)
        {
            if (_currentTargetPos == null || Vector3.Distance(_enemy.transform.position, _currentTargetPos.Value) <= 0.1f)
            {
                List<Vector3> availableTargets = new List<Vector3>(_enemy.PatrolPosList);

                if (_currentTargetPos != null)
                    availableTargets.Remove(_currentTargetPos.Value);

                _currentTargetPos = availableTargets[Random.Range(0, availableTargets.Count)];
            }

            Vector3 direction = _currentTargetPos.Value - _enemy.transform.position;
            direction.Normalize();
            _enemy.GetComponent<CharacterController>().Move(direction * _enemy.MoveSpeedf * Time.deltaTime);
        }
    }

    public void Exit()
    {
        // Logic for exiting Patrol state
    }
}
