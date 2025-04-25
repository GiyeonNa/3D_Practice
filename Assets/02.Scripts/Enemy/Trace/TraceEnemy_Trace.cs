using UnityEngine;

public class TraceEnemy_Trace : FsmState<eEnemyState>
{
    protected Enemy enemy;

    public TraceEnemy_Trace(Enemy enemy) : base(eEnemyState.Trace)
    {
        
    }

    public override void Enter()
    {
        Debug.Log("Enter Trace State");
    }

    public override void Update()
    {
        Debug.Log("Update Trace State");
    }

}
