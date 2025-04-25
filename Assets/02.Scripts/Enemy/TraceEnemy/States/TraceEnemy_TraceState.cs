using UnityEngine;

public class TraceEnemy_TraceState : FsmState<eEnemyState>
{
    private TraceEnemy enemy;
    private GameObject player;

    public TraceEnemy_TraceState(TraceEnemy _enemy) : base(eEnemyState.Trace)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        player = enemy.GetPlayer();
        Debug.Log("Entering Trace State");
    }

    public override void Update()
    {
        Debug.Log("Updating Trace State");
        if (enemy == null || player == null)
        {
            Debug.LogError("Enemy or Player reference is missing");
            return;
        }

        enemy.GetAgent().SetDestination(player.transform.position);
    }

    public override void End()
    {
        Debug.Log("Exiting Trace State");
    }
}
