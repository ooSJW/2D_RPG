using System;
using UnityEngine;
using static EnemyStatData;

public partial class Enemy : CombatObjectBase // Data Field
{
    [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
    [field: SerializeField] public EnemyCombat EnemyCombat { get; private set; }
    [field: SerializeField] public EnemyAnimation EnemyAnimation { get; private set; }
    public Transform playerTransform { get; private set; }
    private EnemyName enemyName;
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

    private EnemyStatInformation enemyStatInformation;
    public EnemyStatInformation EnemyStatInformation
    {
        get => enemyStatInformation;
        private set
        {
            enemyStatInformation = new EnemyStatInformation()
            {
                index = value.index,
                name = value.name,
                ui_name = value.ui_name,
                move_speed = value.move_speed,
                max_hp = value.max_hp,
                min_damage = value.min_damage,
                max_damage = value.max_damage,
                critical_percent = value.critical_percent,
                critical_increase = value.critical_increase,
                attack_delay = value.attack_delay,
                attack_range_x = value.attack_range_x,
                attack_range_y = value.attack_range_y,
                attack_offset_x = value.attack_offset_x,
                attack_offset_y = value.attack_offset_y,
            };
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
        Enum.TryParse<EnemyName>(name, true, out enemyName);
        EnemyStatInformation = MainSystem.Instance.DataManager.EnemyStatData.GetData(enemyName);

        if (MainSystem.Instance.PlayerManager.PlayerAlive())
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
    //private void OnDrawGizmos()
    //{
    //    Vector2 attackRange = new Vector2(EnemyStatInformation.attack_range_x, EnemyStatInformation.attack_range_y);
    //    Gizmos.color = Color.green;
    //    Vector2 offset = new Vector2(Mathf.Abs(EnemyStatInformation.attack_offset_x)
    //        * EnemyAnimation.GetCharacterDirection(), EnemyStatInformation.attack_offset_y);

    //    Gizmos.DrawCube(transform.position + (Vector3)offset, attackRange);
    //}
    private void SetRandomTime()
    {
        randomTime = UnityEngine.Random.Range(MinTime, MaxTime);
    }
    private void RandomState()
    {
        intervalTime += Time.deltaTime;
        if (intervalTime >= randomTime)
        {
            int randomState = UnityEngine.Random.Range((int)EnemyState.Idle, ((int)EnemyState.Move) + 1);
            EnemyState = (EnemyState)randomState;
            EnemyMovement.SetMoveDirection();
            SetRandomTime();
            intervalTime = 0;
        }
    }
    private void SetState()
    {
        switch (EnemyState)
        {
            case EnemyState.Idle:
                RandomState();
                EnemyCombat.CheckAttackRange();
                break;
            case EnemyState.Move:
                RandomState();
                EnemyMovement.Progress();
                EnemyCombat.CheckAttackRange();
                break;
            case EnemyState.Death:
                break;
            default:
                break;
        }
    }
}