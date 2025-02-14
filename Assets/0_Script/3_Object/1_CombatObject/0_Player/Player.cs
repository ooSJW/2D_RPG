using UnityEngine;
using static PlayerStatData;

public partial class Player : CombatObjectBase // Data Field
{
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerAnimation PlayerAnimation { get; private set; }
    [field: SerializeField] public PlayerGroundDetector PlayerGroundDetector { get; private set; }
    [field: SerializeField] public PlayerCombat PlayerCombat { get; private set; }

    // TODO TEST
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 offset;
}

public partial class Player : CombatObjectBase // Data Property
{
    private PlayerStatInformation playerStatInformation;
    public PlayerStatInformation PlayerStatInformation
    {
        get => playerStatInformation;
        set
        {
            playerStatInformation = new PlayerStatInformation()
            {
                index = value.index,
                max_hp = value.max_hp,
                max_exp = value.max_exp,
                max_level = value.max_level,
                min_damage = value.min_damage,
                max_damage = value.max_damage,
                critical_percent = value.critical_percent,
                critical_increase = value.critical_increase,
            };
            Hp = value.max_hp;
            MaxExp = value.max_exp;
        }
    }

    private int level;
    public int Level
    {
        get => level;
        set
        {
            if (level < playerStatInformation.max_level)
            {
                level = value;
                PlayerStatInformation = MainSystem.Instance.DataManager.PlayerStatData.GetData(level.ToString());
            }
        }
    }

    private int exp;
    public int Exp
    {
        get => exp;
        set
        {
            while (value >= maxExp)
            {
                value -= maxExp;
                Level++;
            }
            exp = value;
        }
    }

    private int maxExp;
    public int MaxExp { get => maxExp; set => maxExp = value; }

    private PlayerState playerState;
    public PlayerState PlayerState
    {
        get => playerState;
        set
        {
            if (playerState != value)
            {
                playerState = value;
                switch (playerState)
                {
                    case PlayerState.Attack:
                        PlayerCombat.Combo();
                        break;
                    default:
                        PlayerAnimation.SetAnimationState(playerState);
                        break;
                }
            }
        }
    }
}

public partial class Player : CombatObjectBase // Initialize
{
    private void Allocate()
    {
        level = 1;
        PlayerStatInformation = MainSystem.Instance.DataManager.PlayerStatData.GetData(level.ToString());
    }
    public override void Initialize()
    {
        base.Initialize();
        Allocate();
        Setup();
        PlayerInput.Initialize(this);
        PlayerMovement.Initialize(this);
        PlayerAnimation.Initialize(this);
        PlayerGroundDetector.Initialize(this);
        PlayerCombat.Initialize(this);
    }
    private void Setup()
    {

    }
}
public partial class Player : CombatObjectBase // Main
{
    // TODO TEST
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 dirction = new Vector2(Mathf.Abs(offset.x) * PlayerAnimation.GetCharacterDirection(), offset.y);
        Gizmos.DrawCube(transform.position + (Vector3)dirction, boxSize);
    }
    private void Update()
    {
        PlayerCombat.Progress();
    }
    private void FixedUpdate()
    {
        PlayerMovement.FixedProgress();
    }
    private void LateUpdate()
    {
        PlayerAnimation.LateProgress();
    }
}