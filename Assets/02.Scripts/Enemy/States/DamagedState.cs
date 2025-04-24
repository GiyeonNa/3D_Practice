using UnityEngine;

public class DamagedState : FsmState<eEnemyState>
{
    public DamagedState() : base(eEnemyState.Damaged) { }

    public override void Enter()
    {
        Debug.Log("Entering Damaged State");
    }

    public override void Update()
    {
        Debug.Log("Updating Damaged State");
    }

    public override void End()
    {
        Debug.Log("Exiting Damaged State");
    }
}
