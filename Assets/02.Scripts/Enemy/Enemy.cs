using System.Collections.Generic;
using UnityEngine;

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

public class Enemy : MonoBehaviour, IDamageable
{
    public EnemyState CurrentState = EnemyState.Idle;

    public GameObject player;
    private CharacterController characterController;
    public Vector3 startPos;
    private float attackCurrentTime;
    private float patrolCurrentTime;
    private Vector3? currentTargetPos = null;
    public Vector3 knockbackDirection;

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

    private EnemyFsm enemyFSM; // Add a reference to the EnemyFSM  

    private void Start()
    {
        startPos = transform.position;
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found");
        }

        enemyFSM = new EnemyFsm(this); // Initialize the EnemyFSM with a reference to this Enemy instance  
    }

    private void Update()
    {
        // Delegate behavior to FSM  
        enemyFSM.Update();
    }

    public void TakeDamage(Damage damage)
    {
        Health -= damage.Value;
        if (Health <= 0)
        {
            enemyFSM.ChangeState(eEnemyState.Dead);
        }
        else
        {
            enemyFSM.ChangeState(eEnemyState.Damaged);

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

        // Patrol Points  
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

