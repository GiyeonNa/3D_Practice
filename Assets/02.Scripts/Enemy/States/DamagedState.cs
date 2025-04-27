using UnityEngine;

public class DamagedState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly EnemyFsm _fsm;
    private float _damageTimer;

    public DamagedState(Enemy enemy, EnemyFsm fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }

    public void Enter()
    {
        _damageTimer = 0f;
    }

    public void Execute()
    {
        _damageTimer += Time.deltaTime;
        if (_damageTimer >= _enemy.DamagedDelayTime)
        {
            _fsm.ChangeState(eEnemyState.Trace);
        }

        _enemy.GetComponent<CharacterController>().Move(_enemy.knockbackDirection * _enemy.KnockbackForce * Time.deltaTime);
    }

    public void Exit()
    {
        // Logic for exiting Damaged state
    }
}
