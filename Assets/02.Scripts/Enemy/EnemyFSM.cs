using System;
using UnityEngine;

public class EnemyFSM
{
    private Fsm<eEnemyState> fsm;
    public Enemy enemy;

    public EnemyFSM(Enemy _enemy)
    {
        enemy = _enemy;
        fsm = FsmFactory.CreateFsm<eEnemyState>();

        // Add states
        FsmFactory.AddState(fsm, new IdleState(enemy));
        FsmFactory.AddState(fsm, new TraceState());
        FsmFactory.AddState(fsm, new PatrolState());
        FsmFactory.AddState(fsm, new AttackState());
        FsmFactory.AddState(fsm, new ReturnState());
        FsmFactory.AddState(fsm, new DamagedState());
        FsmFactory.AddState(fsm, new DeadState());

        // Set initial state
        FsmFactory.SetInitialState(fsm, eEnemyState.Idle);
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
