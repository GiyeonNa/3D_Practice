using UnityEngine;
using System.Collections;

public abstract class FsmState<T>
{
    private T state;

    public FsmState(T _state)
    {
        state = _state;
    }

    public T getState
    {
        get
        {
            return state;
        }
    }

    #region - virtaul 
    public virtual void Enter()
    {
    }
    public virtual void Update()
    {
    }
    public virtual void End()
    {
    }
    #endregion
}
