using UnityEngine;

public class EnemyFsm : Fsm<eEnemyState>
{
    public Enemy traceEnemy;

    public EnemyFsm(Enemy traceEnemy)
    {
        this.traceEnemy = traceEnemy;
    }

    public override void Update()
    {
        base.Update();
    }

    public bool IsState(eEnemyState state)
    {
        if (curState.getState.Equals(state))
            return true;

        if (nextState.getState.Equals(state))
            return true;

        return false;
    }
}
