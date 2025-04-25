using UnityEngine;

public abstract class EnemyFsmFactory 
{
    public abstract EnemyFsm CreateEnemyFsm(Enemy enemy);
}
