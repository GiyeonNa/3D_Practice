using UnityEngine;

public class DeadState : IEnemyState
{
    private readonly Enemy enemy;
    private readonly EnemyFsm fsm;

    public DeadState(Enemy enemy, EnemyFsm fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public void Enter()
    {
        enemy.animator.SetTrigger("Die");

        if (enemy is EliteEnemy eliteEnemy)
        {
            eliteEnemy.TriggerExplosion();
        }

        GameObject.Destroy(enemy.gameObject);
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
