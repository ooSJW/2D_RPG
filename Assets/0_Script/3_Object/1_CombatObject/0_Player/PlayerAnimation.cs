using UnityEngine;

public partial class PlayerAnimation : MonoBehaviour // Data Field
{
    private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
}
public partial class PlayerAnimation : MonoBehaviour // Initialize
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
public partial class PlayerAnimation : MonoBehaviour // Property
{
    public void SetAnimationState(PlayerState playerState)
    {
        animator.SetInteger("State", (int)playerState);
    }
    public void AttackAnimationTrigger(int attackAnimationIndex = 0)
    {
        string trigger = $"Attack{attackAnimationIndex}";
        animator.SetTrigger(trigger);
    }
    public void FlipX()
    {
        if (!player.PlayerCombat.IsAttacking)
        {
            float moveXValue = player.PlayerInput.InputVector.x;

            if (moveXValue == 0) return;
            else if (moveXValue > 0) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
        }
    }
    public float GetCharacterDirection()
    {
        if (spriteRenderer.flipX)
            return -1;
        else return 1;
    }
}
public partial class PlayerAnimation : MonoBehaviour // Main
{
    public void LateProgress()
    {
        FlipX();
    }
}