using UnityEngine;

public class TraceEnemyFSM : MonoBehaviour
{
    private Fsm<eEnemyState> fsm;
    private TraceEnemy enemy;

    public void MakeFSM(TraceEnemy _enemy)
    {
        enemy = _enemy;
        fsm = FsmFactory.CreateFsm<eEnemyState>();

        // Add states
        FsmFactory.AddState(fsm, new TraceEnemy_TraceState(enemy));

        // Set initial state
        FsmFactory.SetInitialState(fsm, eEnemyState.Trace);
    }

    public void Update()
    {
        fsm.Update();
    }

    public void SetState(eEnemyState state)
    {
        fsm.SetState(state);
    }
}
