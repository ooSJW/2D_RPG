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
                enemyState = value;
                if (enemyState == EnemyState.Death)
                    Death();
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
                attack_range = value.attack_range,
                attack_offset = value.attack_offset,
            };
            hp = enemyStatInformation.max_hp;
        }
    }

    public override int Hp
    {
        get => hp;
        protected set
        {
            int hpTemp = hp;
            hp = value;
            if (hpTemp <= hp)
            {
                // TODO heal
            }
            else if (hpTemp > hp && hp > 0)
            {

            }
            else
                EnemyState = EnemyState.Death;
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

        EnemyState = EnemyState.Idle;
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
        if (EnemyState != EnemyState.Death)
            SetState();
    }
    private void LateUpdate()
    {
        if (EnemyState != EnemyState.Death)
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
    private void Death()
    {
        EnemyAnimation.Death();
        MainSystem.Instance.EnemyManager.SigndownEnemy(this);
    }
    public void Despawn()
    {
        MainSystem.Instance.PoolManager.Despawn(gameObject);
    }
}