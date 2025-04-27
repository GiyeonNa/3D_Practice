using System.Collections.Generic;

public class TraceEnemyFsm : EnemyFsm
{
    public TraceEnemyFsm(Enemy enemy) : base(enemy)
    {
        // Override the initial state to Trace
        _currentState = _states[eEnemyState.Trace];
    }
}
