using UnityEngine;

public partial class PlayerCombat : MonoBehaviour // Data Field
{
    private Player player;
    private int comboCount;

    [SerializeField] private LayerMask targetLayer;

    // TODO TEST : 추후 csv json으로 작성
    private const int maxAttackCount = 3;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private Vector2 offset;
}
public partial class PlayerCombat : MonoBehaviour // Data Property
{
    private bool isAttacking;
    public bool IsAttacking
    {
        get => isAttacking;
        set
        {
            isAttacking = value;
            player.PlayerMovement.CanMove = !isAttacking;
            if (isAttacking)
                player.PlayerState = PlayerState.Attack;
            else
                player.PlayerState = PlayerState.Idle;
        }
    }
}
public partial class PlayerCombat : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize(Player playerValue)
    {
        player = playerValue;
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class PlayerCombat : MonoBehaviour // Main
{
    public void Progress()
    {
        Attack();
    }
}

public partial class PlayerCombat : MonoBehaviour // Property
{
    private void Attack()
    {
        if (player.PlayerInput.IsAttackPressed())
        {
            player.PlayerState = PlayerState.Attack;
            IsAttacking = true;
        }
    }

    public void OnAttack()
    {
        Vector3 attackPosition = transform.position;
        attackPosition.x += Mathf.Abs(offset.x) * player.PlayerAnimation.GetCharacterDirection();
        attackPosition.y += offset.y;

        Collider2D[] hitEnemyArray = Physics2D.OverlapBoxAll(attackPosition, boxSize, 0, targetLayer);
        if (hitEnemyArray != null)
        {
            for (int i = 0; i < hitEnemyArray.Length; i++)
            {
                Enemy hitEnemy = hitEnemyArray[i].GetComponent<Enemy>();
                player.SendDamage(10, hitEnemy);
            }
        }

    }

    public void Combo()
    {
        if (player.PlayerInput.IsAttackPressed() && comboCount < maxAttackCount)
        {
            player.PlayerAnimation.AttackAnimationTrigger(comboCount);
            comboCount++;
        }
        else
            EndAttack();
    }
    public void EndAttack()
    {
        IsAttacking = false;
        comboCount = 0;
    }
}