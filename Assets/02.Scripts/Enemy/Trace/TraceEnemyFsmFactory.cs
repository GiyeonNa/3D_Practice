using UnityEngine;

public class TraceEnemyFsmFactory : EnemyFsmFactory
{
    public override EnemyFsm CreateEnemyFsm(Enemy enemy)
    {
        EnemyFsm enemyFsm = new EnemyFsm(enemy);
        enemyFsm.AddFsm(new TraceEnemy_Trace(enemy));
        return enemyFsm;
    }
}

