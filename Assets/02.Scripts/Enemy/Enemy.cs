using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

public class Enemy : MonoBehaviour, IDamageable, IPoolObject
{
    public GameObject player;
    protected CharacterController characterController; // Changed from private to protected
    protected EnemyFsm enemyFSM; // Changed from private to protected

    public Animator animator; // Changed from private to protected

    public Vector3 startPos;
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

    // Change access modifier to protected for healthSlider
    [SerializeField]
    protected Slider healthSlider;


    private void Start()
    {
        InitializeEnemy();
    }

    private void Update()
    {
        enemyFSM.Update();
    }

    public virtual void TakeDamage(Damage damage) // Marked as virtual to allow overriding
    {
        ApplyDamage(damage);
    }

    public IEnumerator ChangeAllChildColorsTemporarily(Color newColor, float duration)
    {
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();
        Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

        // Store original colors and change to the new color
        foreach (Renderer renderer in childRenderers)
        {
            if (renderer != null)
            {
                originalColors[renderer] = renderer.material.color;
                renderer.material.color = newColor;
            }
        }

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Revert to original colors
        foreach (var kvp in originalColors)
        {
            if (kvp.Key != null)
            {
                kvp.Key.material.color = kvp.Value;
            }
        }
    }

    private void OnDrawGizmos()
    {
        DrawGizmos();
    }

    // Initialization logic
    protected virtual void InitializeEnemy() // Changed from private to protected virtual
    {
        startPos = transform.position;
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyFSM = new EnemyFsm(this);
    }


    private void ApplyDamage(Damage damage)
    {
        Health -= damage.Value;

        // Update the health slider
        if (healthSlider != null)
        {
            healthSlider.value = (float)Health / 100; // Assuming max health is 100
        }

        if (Health <= 0)
        {
            enemyFSM.ChangeState(eEnemyState.Dead);
        }
        else
        {
            enemyFSM.ChangeState(eEnemyState.Damaged);
            CalculateKnockback(damage);
        }
    }


    private void CalculateKnockback(Damage damage)
    {
        if (damage.From != null)
        {
            knockbackDirection = (transform.position - damage.From.transform.position).normalized;
        }
        else
        {
            knockbackDirection = Vector3.zero;
        }
    }

    protected virtual void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FindDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ReturnDistance);

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

    public void OnCreatedInPool()
    {
        // Perform any initialization logic specific to when the object is created in the pool
        Debug.Log("Enemy created in pool.");
    }

    public void OnGettingFromPool()
    {
        // Reset the state of the enemy when retrieved from the pool
        Debug.Log("Enemy retrieved from pool.");
        InitializeEnemy(); // Reset position, health, and other properties

        // Reset health slider if applicable
        if (healthSlider != null)
        {
            healthSlider.value = 1.0f; // Assuming max health is 100
        }

        // Reset animations or other state-specific data
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }
    }
}

