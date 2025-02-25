using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public partial class PlayerAnimation : MonoBehaviour // Data Field
{
    private Player player;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Color originColor;
}
public partial class PlayerAnimation : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        originColor = spriteRenderer.color;
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
    public void Blink()
    {
        if (originColor == spriteRenderer.color)
            spriteRenderer.color = originColor * 0.5f;
        else
            spriteRenderer.color = originColor;
    }
    public void EndBlink()
    {
        spriteRenderer.color = originColor;
    }
    public void PlayerDeath()
    {
        animator.SetInteger("State", (int)PlayerState.Death);
        animator.SetTrigger(PlayerState.Death.ToString());
    }

}
public partial class PlayerAnimation : MonoBehaviour // Main
{
    public void LateProgress()
    {
        FlipX();
    }
}