using UnityEngine;

public class DeadState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly EnemyFsm _fsm;

    public DeadState(Enemy enemy, EnemyFsm fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }

    public void Enter()
    {
        GameObject.Destroy(_enemy.gameObject);
    }

    public void Execute()
    {
        // Logic for updating Dead state
    }

    public void Exit()
    {
        // Logic for exiting Dead state
    }
}
