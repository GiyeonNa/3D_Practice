using UnityEngine;

public class TraceState : FsmState<eEnemyState>
{
    public TraceState() : base(eEnemyState.Trace) { }

    public override void Enter()
    {
        Debug.Log("Entering Trace State");
    }

    public override void Update()
    {
        Debug.Log("Updating Trace State");
    }

    public override void End()
    {
        Debug.Log("Exiting Trace State");
    }
}
