using UnityEngine;

public class DeadState : FsmState<eEnemyState>
{
    public DeadState() : base(eEnemyState.Dead) { }

    public override void Enter()
    {
        Debug.Log("Entering Dead State");
    }

    public override void Update()
    {
        Debug.Log("Updating Dead State");
    }

    public override void End()
    {
        Debug.Log("Exiting Dead State");
    }
}
