using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyFsm EnemyFSM;
    protected GameObject player;
    protected CharacterController characterController;
    protected Vector3 startPos;
    protected float attackCurrentTime;
    protected float patrolCurrentTime;
    protected Vector3? currentTargetPos = null;
    protected Vector3 knockbackDirection;
    protected NavMeshAgent agent;

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
   

    protected virtual void Start()
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

    public void TakeDamage(Damage damage)
    {
        Health -= damage.Value;
    }

    public virtual GameObject GetPlayer()
    {
        return player;
    }

    public virtual NavMeshAgent GetAgent()
    {
        return agent;
    }

    protected virtual void OnDrawGizmos()
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
