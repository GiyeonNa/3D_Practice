using UnityEngine;

public class DamagedState : IEnemyState
{
    private readonly Enemy enemy;
    private readonly EnemyFsm fsm;
    private float damageTimer;

    public DamagedState(Enemy enemy, EnemyFsm fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public void Enter()
    {
        enemy.animator.SetTrigger("Hit");
        damageTimer = 0f;
    }

    public void Execute()
    {
        damageTimer += Time.deltaTime;
        if (damageTimer >= enemy.DamagedDelayTime)
        {
            fsm.ChangeState(eEnemyState.Trace);
        }

        enemy.GetComponent<CharacterController>().Move(enemy.knockbackDirection * enemy.KnockbackForce * Time.deltaTime);
    }

    public void Exit()
    {
        // Logic for exiting Damaged state
    }
}
