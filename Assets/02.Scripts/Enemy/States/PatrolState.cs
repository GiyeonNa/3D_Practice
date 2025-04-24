using UnityEngine;

public class PatrolState : FsmState<eEnemyState>
{
    public PatrolState() : base(eEnemyState.Patrol) { }

    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void Update()
    {
        Debug.Log("Updating Patrol State");
    }

    public override void End()
    {
        Debug.Log("Exiting Patrol State");
    }
}
