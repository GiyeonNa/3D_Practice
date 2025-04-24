using UnityEngine;

public enum eEnemyState
{
    Idle,
    Trace,
    Patrol,
    Attack,
    Return,
    Damaged,
    Dead
}


public class EnemyState : FsmState<eEnemyState>
{
    protected Enemy enemy;

    public EnemyState(eEnemyState _state) : base(_state)
    {
    }
}
