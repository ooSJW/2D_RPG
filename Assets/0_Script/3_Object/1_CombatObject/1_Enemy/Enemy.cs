using UnityEngine;

public partial class Enemy : CombatObjectBase // Data Field
{
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
    [field: SerializeField] public EnemyCombat EnemyCombat { get; private set; }
    [field: SerializeField] public EnemyAnimation EnemyAnimation { get; private set; }
    public Transform playerTransform { get; private set; }
}
public partial class Enemy : CombatObjectBase // Data Property
{
    private EnemyState enemyState;
    public EnemyState EnemyState
    {
        get => enemyState;
        set
        {
            if (enemyState != value)
            {
                // TODO 이동 공격 및 애니메이션 추가
                enemyState = value;
                switch (enemyState)
                {
                    case EnemyState.Idle:
                        break;
                    case EnemyState.Move:
                        break;
                    case EnemyState.Attack:
                        break;
                    case EnemyState.Death:
                        break;
                }
            }
        }
    }

    // random state time
    [field: SerializeField] public float MinTime { get; set; }
    [field: SerializeField] public float MaxTime { get; set; }
    private float randomTime;
    private float intervalTime;
}
public partial class Enemy : CombatObjectBase // Initialize
{
    private void Allocate()
    {
        playerTransform = MainSystem.Instance.PlayerManager.Player.transform;
    }
    public override void Initialize()
    {
        base.Initialize();
        Allocate();
        Setup();
        EnemyMovement.Initialize(this);
        EnemyCombat.Initialize(this);
        EnemyAnimation.Initialize(this);
    }
    private void Setup()
    {
        SetRandomTime();
    }
}
public partial class Enemy : CombatObjectBase // Main
{
    private void Update()
    {
        SetState();
    }
    private void LateUpdate()
    {
        EnemyAnimation.LateProgress();
    }
}
public partial class Enemy : CombatObjectBase // Property
{
    private void SetRandomTime()
    {
        randomTime = Random.Range(MinTime, MaxTime);
    }
    private void RandomState()
    {
        intervalTime += Time.deltaTime;
        if (intervalTime >= randomTime)
        {
            int randomState = Random.Range((int)EnemyState.Idle, ((int)EnemyState.Move) + 1);
            EnemyState = (EnemyState)randomState;
            EnemyMovement.SetMoveDirection();
            SetRandomTime();
            intervalTime = 0;
        }
    }
    private void SetState()
    {
        // TODO 여기하던 중 , 가능하면 코드 보수 및 깔끔하게 간소화ㄱㄱ
        switch (EnemyState)
        {
            case EnemyState.Idle:
                RandomState();
                break;
            case EnemyState.Move:
                RandomState();
                EnemyMovement.Progress();
                break;
            case EnemyState.Attack:
                break;
            case EnemyState.Death:
                break;
            default:
                break;
        }
    }
}