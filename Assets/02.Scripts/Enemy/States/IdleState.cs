using UnityEngine;

public class IdleState : FsmState<eEnemyState>
{
    private Enemy enemy;

    public IdleState(Enemy _enemy) : base(eEnemyState.Idle)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public override void Update()
    {
        Debug.Log("Updating Idle State");
    }

    public override void End()
    {
        Debug.Log("Exiting Idle State");
    }
}
