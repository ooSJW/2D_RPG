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
    public void SetAnimation()
    {
        animator.SetInteger("State", (int)player.PlayerState);
    }
    public void FlipX()
    {
        float moveXValue = player.PlayerInput.InputVector.x;

        if (moveXValue == 0) return;
        else if (moveXValue > 0) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }
}
public partial class PlayerAnimation : MonoBehaviour // Main
{
    public void LateProgress()
    {
        SetAnimation();
        FlipX();
    }
}