using UnityEngine;

public class IdleState : FsmState<eEnemyState>
{

    public IdleState() : base(eEnemyState.Idle)
    {
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
