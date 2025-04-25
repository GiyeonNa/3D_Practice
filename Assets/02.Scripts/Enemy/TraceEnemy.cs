using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class TraceEnemy : Enemy
{
    private TraceEnemyFSM traceEnemyFSM;


    private void Awake()
    {
        traceEnemyFSM = GetComponent<TraceEnemyFSM>();
        traceEnemyFSM.MakeFSM(this);
    }

    protected override void Start()
    { 
        base.Start();
    }

    public TraceEnemyFSM GetFSM()
    {
        return traceEnemyFSM;
    }

}
