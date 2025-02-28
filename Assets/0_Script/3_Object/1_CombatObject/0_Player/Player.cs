using System.Collections;
using UnityEngine;
using static PlayerStatData;

public partial class Player : CombatObjectBase // Data Field
{
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerAnimation PlayerAnimation { get; private set; }
    [field: SerializeField] public PlayerGroundDetector PlayerGroundDetector { get; private set; }
    [field: SerializeField] public PlayerCombat PlayerCombat { get; private set; }

    [SerializeField] private Collider2D playerCollider;

    [SerializeField] private float invincibilityDuration;
    [SerializeField] private float blinkTime;
    private float intervalTime;

    // TODO TEST
    private Vector2 boxSize;
    private Vector2 offset;
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
                move_speed = value.move_speed,
                jump_force = value.jump_force,
                critical_percent = value.critical_percent,
                critical_increase = value.critical_increase,
                attack_range = value.attack_range,
                attack_offset = value.attack_offset,
            };
            Hp = value.max_hp;
            MaxHp = value.max_hp;
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
                if (playerState != PlayerState.Death)
                {
                    playerState = value;
                    switch (playerState)
                    {
                        case PlayerState.Attack:
                            PlayerCombat.Combo();
                            break;
                        case PlayerState.Death:
                            Death();
                            break;
                        default:
                            PlayerAnimation.SetAnimationState(playerState);
                            break;
                    }
                }
            }
        }
    }

    private int maxHp;
    public int MaxHp { get => maxHp; private set => maxHp = value; }

    public override int Hp
    {
        get => hp;
        protected set
        {
            int hpTemp = hp;
            if (hp != value)
            {
                hp = value;
                if (hpTemp <= value)
                {
                    // Heal;
                }
                else if (hpTemp > value && value > 0)
                    StartCoroutine(GetDamage());
                else
                {
                    hp = 0;
                    PlayerState = PlayerState.Death;
                }
            }
        }
    }

    private bool isInvincible;
    public bool IsInvincible { get => isInvincible; private set => isInvincible = value; }
}

public partial class Player : CombatObjectBase // Initialize
{
    private void Allocate()
    {
        level = 1;
        PlayerStatInformation = MainSystem.Instance.DataManager.PlayerStatData.GetData(level.ToString());
        boxSize = new Vector2(PlayerStatInformation.attack_range[0], PlayerStatInformation.attack_range[1]);
        offset = new Vector2(PlayerStatInformation.attack_offset[0], PlayerStatInformation.attack_offset[1]);
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
    private void Update()
    {
        if (PlayerState != PlayerState.Death)
        {
            PlayerCombat.Progress();
            PlayerGroundDetector.Progress();
        }
    }
    private void FixedUpdate()
    {
        if (PlayerState != PlayerState.Death)
            PlayerMovement.FixedProgress();
    }
    private void LateUpdate()
    {
        if (PlayerState != PlayerState.Death)
            PlayerAnimation.LateProgress();
    }
}
public partial class Player : CombatObjectBase // Property
{
    private IEnumerator GetDamage()
    {
        playerCollider.enabled = false;
        while (invincibilityDuration > intervalTime)
        {
            PlayerAnimation.Blink();
            intervalTime += blinkTime;
            yield return new WaitForSeconds(blinkTime);
        }

        intervalTime = 0;
        PlayerAnimation.EndBlink();
        playerCollider.enabled = true;
    }
    private void Death()
    {
        PlayerInput.InputAbleSetting(false);
        playerCollider.enabled = false;
        PlayerAnimation.PlayerDeath();
        MainSystem.Instance.PlayerManager.SigndownPlayer();
        this.enabled = false;
    }
}