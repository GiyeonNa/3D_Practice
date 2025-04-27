using UnityEngine;

public class TraceEnemy : Enemy
{
    protected override void InitializeEnemy()
    {
        startPos = transform.position;
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyFSM = new TraceEnemyFsm(this); // Use TraceEnemyFsm for this enemy
    }
}
