using System;
using System.Collections.Generic;

public class EnemyFsm
{
    private readonly Enemy _enemy;
    private IEnemyState _currentState;
    private readonly Dictionary<eEnemyState, IEnemyState> _states;

    public EnemyFsm(Enemy enemy)
    {
        _enemy = enemy;
        _states = new Dictionary<eEnemyState, IEnemyState>
        {
            { eEnemyState.Idle, new IdleState(_enemy, this) },
            { eEnemyState.Trace, new TraceState(_enemy, this) },
            { eEnemyState.Patrol, new PatrolState(_enemy, this) },
            { eEnemyState.Attack, new AttackState(_enemy, this) },
            { eEnemyState.Return, new ReturnState(_enemy, this) },
            { eEnemyState.Damaged, new DamagedState(_enemy, this) },
            { eEnemyState.Dead, new DeadState(_enemy, this) }
        };

        _currentState = _states[eEnemyState.Idle];
    }

    public void ChangeState(eEnemyState newState)
    {
        _currentState.Exit();
        _currentState = _states[newState];
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState.Execute();
    }
}

public interface IEnemyState
{
    void Enter();
    void Execute();
    void Exit();
}
