using System;
using System.Collections.Generic;

public static class FsmFactory
{
    public static Fsm<T> CreateFsm<T>()
    {
        return new Fsm<T>();
    }

    public static void AddState<T>(Fsm<T> fsm, FsmState<T> state)
    {
        if (fsm == null || state == null)
            throw new ArgumentNullException("FSM or state cannot be null.");
    
        fsm.AddFsm(state);
    }

    public static void SetInitialState<T>(Fsm<T> fsm, T initialState)
    {
        if (fsm == null)
            throw new ArgumentNullException("FSM cannot be null.");

        fsm.SetState(initialState);
    }
}
