using UnityEngine;

public class ReturnState : FsmState<eEnemyState>
{
    public ReturnState() : base(eEnemyState.Return) { }

    public override void Enter()
    {
        Debug.Log("Entering Return State");
    }

    public override void Update()
    {
        Debug.Log("Updating Return State");
    }

    public override void End()
    {
        Debug.Log("Exiting Return State");
    }
}
