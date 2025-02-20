using UnityEngine;

public partial class EnemyAnimation : MonoBehaviour // Data Field
{
    private Enemy enemy;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
}
public partial class EnemyAnimation : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize(Enemy enemyValue)
    {
        enemy = enemyValue;
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class EnemyAnimation : MonoBehaviour // Main
{
    public void LateProgress()
    {
        SetAnimationState();
        FlipX();
    }
}
public partial class EnemyAnimation : MonoBehaviour // Property
{
    private void SetAnimationState()
    {
        animator.SetInteger("State", (int)enemy.EnemyState);
    }
    private void FlipX()
    {
        float moveDirection = enemy.EnemyMovement.MoveDirection;
        if (moveDirection > 0)
            spriteRenderer.flipX = false;
        else if (moveDirection < 0)
            spriteRenderer.flipX = true;
    }
}