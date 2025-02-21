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
        RandomAttack();
        animator.SetInteger("State", (int)enemy.EnemyState);
    }
    private void RandomAttack()
    {
        if (enemy.EnemyState == EnemyState.Attack)
            animator.SetInteger("RandomAttack", Random.Range(0, 2));
    }
    public void FlipX()
    {
        if (enemy.EnemyState != EnemyState.Attack)
        {
            float moveDirection = enemy.EnemyMovement.MoveDirection;
            if (moveDirection > 0)
                spriteRenderer.flipX = false;
            else if (moveDirection < 0)
                spriteRenderer.flipX = true;
        }
    }

    public void FlipX(float xDirection)
    {
        if (xDirection > 0)
            spriteRenderer.flipX = false;
        else if (xDirection < 0)
            spriteRenderer.flipX = true;
    }

    public float GetCharacterDirection()
    {
        if (spriteRenderer.flipX)
            return -1;
        else return 1;
    }
}