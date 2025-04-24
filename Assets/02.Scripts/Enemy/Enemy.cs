using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Trace,
    Patrol,
    Attack,
    Return,
    Damaged,
    Dead
}

public class Enemy : MonoBehaviour
{
    public EnemyState CurrentState = EnemyState.Idle;

    private GameObject player;
    private CharacterController characterController;
    private Vector3 startPos;
    private float attackCurrentTime;
    private float patrolCurrentTime;
    private Vector3? currentTargetPos = null;
    private Vector3 knockbackDirection;
    private NavMeshAgent agent;

    [Header("Enemy Info")]
    public int Health;
    public float FindDistance;
    public float ReturnDistance;
    public float MoveSpeedf;
    public float AttackDistance;
    public float AttackDelayTime;
    public float DamagedDelayTime;
    public float DamageTimer;
    public List<Vector3> PatrolPosList = new List<Vector3>(); 
    public float PatrolChangeTime;
    public float KnockbackForce;
   

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        startPos = transform.position;
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found");
        }
    }

    private void Update()
    {
        switch(CurrentState)
        {
            case EnemyState.Idle:
            {
                Idle();
                break;
            }
            case EnemyState.Trace:
            {
                Trace();
                break;
            }
            case EnemyState.Patrol:
            {
                Patrol();
                break;
            }
            case EnemyState.Attack:
            {
                Attack();
                break;
            }
            case EnemyState.Return:
            {
                Return();
                break;
            }
            case EnemyState.Damaged:
            {
                Damaged();
                break;
            }
            case EnemyState.Dead:
            {
                Dead();
                break;
            }

        }
    }

    #region State Methods
    private void Idle()
    {
        patrolCurrentTime += Time.deltaTime;
        if (Vector3.Distance(transform.position, player.transform.position) < FindDistance)
        {
            patrolCurrentTime = 0f;
            CurrentState = EnemyState.Trace;
        }

        if (patrolCurrentTime >= PatrolChangeTime)
        {
            patrolCurrentTime = 0f;
            CurrentState = EnemyState.Patrol;
        }
    }

    private void Trace()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= ReturnDistance)
        {
            CurrentState = EnemyState.Return;
            return;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < AttackDistance)
        {
            CurrentState = EnemyState.Attack;
            return;
        }

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        //characterController.Move(direction * MoveSpeedf * Time.deltaTime);
        agent.SetDestination(player.transform.position);


    }
    private void Patrol()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < FindDistance)
        {
            CurrentState = EnemyState.Trace;
            return;
        }

        if (PatrolPosList.Count > 0)
        {
            if (currentTargetPos == null || Vector3.Distance(transform.position, currentTargetPos.Value) <= 0.1f)
            {
                List<Vector3> availableTargets = new List<Vector3>(PatrolPosList);

                if (currentTargetPos != null)
                    availableTargets.Remove(currentTargetPos.Value);

                currentTargetPos = availableTargets[Random.Range(0, availableTargets.Count)];
            }

            Vector3 direction = currentTargetPos.Value - transform.position;
            direction.Normalize();
            characterController.Move(direction * MoveSpeedf * Time.deltaTime);
        }
    }
    private void Attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= AttackDistance)
        {
            CurrentState = EnemyState.Trace;
            //attackCurrentTime = 0f;
            return;
        }
        if (attackCurrentTime <= 0)
        {
            Debug.Log("Attack");
            attackCurrentTime = AttackDelayTime;
        }
        else
        {
            attackCurrentTime -= Time.deltaTime;
        }
    }
    private void Return()
    {
        if (Vector3.Distance(transform.position, startPos) <= 0.1f)
        {
            transform.position = startPos;
            CurrentState = EnemyState.Idle;
            return;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < FindDistance)
        {
            CurrentState = EnemyState.Trace;
            return;
        }

        Vector3 direction = startPos - transform.position;
        direction.Normalize();
        //characterController.Move(direction * MoveSpeedf * Time.deltaTime);
        agent.SetDestination(startPos); 

    }
    private void Damaged()
    {
        DamageTimer += Time.deltaTime;
        if (DamageTimer >= DamagedDelayTime)
        {
            DamageTimer = 0f;
            CurrentState = EnemyState.Trace;
        }
        characterController.Move(knockbackDirection * KnockbackForce * Time.deltaTime);
    }
    private void Dead()
    {
        Debug.Log("Enemy is dead");
        Destroy(gameObject);
    }
    #endregion

    public void TakeDamage(Damage damage)
    {
        Health -= damage.Value;
        if (Health <= 0)
        {
            CurrentState = EnemyState.Dead;
        }
        else
        {
            CurrentState = EnemyState.Damaged;

            // Calculate knockback direction
            if (damage.From != null)
                knockbackDirection = (transform.position - damage.From.transform.position).normalized;
            else
                knockbackDirection = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FindDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ReturnDistance);


        //Patrol Points를 보여주기
        Gizmos.color = Color.yellow;
        for (int i = 0; i < PatrolPosList.Count; i++)
        {
            Gizmos.DrawSphere(PatrolPosList[i], 0.5f);
            if (i < PatrolPosList.Count - 1)
            {
                Gizmos.DrawLine(PatrolPosList[i], PatrolPosList[i + 1]);
            }
        }

    }


}
