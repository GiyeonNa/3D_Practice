using UnityEngine;

public class TraceState : IEnemyState
{
    private readonly Enemy enemy;
    private readonly EnemyFsm fsm;

    public TraceState(Enemy enemy, EnemyFsm fsm)
    {
        this.enemy = enemy;
        this.fsm = fsm;
    }

    public void Enter()
    {
        enemy.animator.SetTrigger("IdleToMove");
        // Logic for entering Trace state
    }

    public void Execute()
    {
        //if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) >= enemy.ReturnDistance)
        //{
        //    fsm.ChangeState(eEnemyState.Return);
        //    return;
        //}

        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.AttackDistance)
        {
            // Check if the enemy is an EliteEnemy and trigger SpecialAttack with a 20% chance
            if (enemy is EliteEnemy /*&& Random.value <= 0.2f*/)
            {
                fsm.ChangeState(eEnemyState.SpecialAttack);
                return;
            }

            fsm.ChangeState(eEnemyState.Attack);
            return;
        }

        Vector3 direction = enemy.player.transform.position - enemy.transform.position;
        direction.Normalize();
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f); // 5f는 회전 속도
        }
        enemy.GetComponent<CharacterController>().Move(direction * enemy.MoveSpeedf * Time.deltaTime);
    }

    public void Exit()
    {
        // Logic for exiting Trace state
    }
}
