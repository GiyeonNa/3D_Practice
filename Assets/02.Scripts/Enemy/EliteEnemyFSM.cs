using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyFSM : EnemyFsm
{
    public EliteEnemyFSM(EliteEnemy eliteEnemy) : base(eliteEnemy) 
    {
        _enemy = eliteEnemy;
        _states = new Dictionary<eEnemyState, IEnemyState>
        {
            { eEnemyState.Idle, new IdleState(_enemy, this) },
            { eEnemyState.Trace, new TraceState(_enemy, this) },
            { eEnemyState.Patrol, new PatrolState(_enemy, this) },
            { eEnemyState.Attack, new AttackState(_enemy, this) },
            { eEnemyState.SpecialAttack, new SpecialAttackState(_enemy, this)},
            { eEnemyState.Return, new ReturnState(_enemy, this) },
            { eEnemyState.Damaged, new DamagedState(_enemy, this) },
            { eEnemyState.Dead, new DeadState(_enemy, this) }
        };

        _currentState = _states[eEnemyState.Idle];
    }
}
