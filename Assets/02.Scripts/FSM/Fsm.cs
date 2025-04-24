using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fsm<T>
{
    protected SortedList<T, FsmState<T>> stateList = new SortedList<T, FsmState<T>>();
    protected FsmState<T> curState = null;
    protected FsmState<T> nextState = null;

    #region - virtual
    public virtual void Clear()
    {
        stateList.Clear();
        curState = null;
        nextState = null;
    }

    public virtual void AddFsm(FsmState<T> _state)
    {
        if (stateList.ContainsKey(_state.getState))
            return;
        stateList.Add(_state.getState, _state);
    }

    public virtual void SetState(T _state)
    {
        if (!stateList.ContainsKey(_state))
            return;
        nextState = stateList[_state];
    }

    public virtual void Update()
    {
        if (nextState != null)
        {
            curState?.End();

            curState = nextState;
            nextState = null;
            curState.Enter();
        }

        curState?.Update();
    }
    #endregion
}