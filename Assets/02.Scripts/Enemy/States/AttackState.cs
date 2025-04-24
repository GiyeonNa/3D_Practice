using UnityEngine;

public class AttackState : FsmState<eEnemyState>
{
    public AttackState() : base(eEnemyState.Attack) { }

    public override void Enter()
    {
        Debug.Log("Entering Attack State");
    }

    public override void Update()
    {
        Debug.Log("Updating Attack State");
    }

    public override void End()
    {
        Debug.Log("Exiting Attack State");
    }
}
