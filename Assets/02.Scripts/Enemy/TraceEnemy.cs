using UnityEngine;


public class TraceEnemy : Enemy
{
    private void Awake()
    {
        // FSM �ʱ�ȭ
        TraceEnemyFsmFactory fsmFactory = new TraceEnemyFsmFactory();
        EnemyFSM = fsmFactory.CreateEnemyFsm(this);
    }

    protected override void Start()
    {
        base.Start();
        // �ʱ� ���� ���� (��: Trace ����)
        EnemyFSM.SetState(eEnemyState.Trace);
    }

    private void Update()
    {
        // FSM ������Ʈ
        EnemyFSM.Update();
    }
}
