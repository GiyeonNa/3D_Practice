using UnityEngine;


public class TraceEnemy : Enemy
{
    private void Awake()
    {
        // FSM 초기화
        TraceEnemyFsmFactory fsmFactory = new TraceEnemyFsmFactory();
        EnemyFSM = fsmFactory.CreateEnemyFsm(this);
    }

    protected override void Start()
    {
        base.Start();
        // 초기 상태 설정 (예: Trace 상태)
        EnemyFSM.SetState(eEnemyState.Trace);
    }

    private void Update()
    {
        // FSM 업데이트
        EnemyFSM.Update();
    }
}
