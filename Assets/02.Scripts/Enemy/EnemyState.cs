using UnityEngine;

public class EnemyState : FsmState<eEnemyState>
{
    protected Enemy enemy;

    public EnemyState(eEnemyState _state) : base(_state)
    {
    }
}
