using UnityEngine;

public partial class EnemyMovement : MonoBehaviour // Data Field
{
    private Enemy enemy;


    [field: SerializeField] public float MoveDirection { get; private set; }
    [SerializeField] private LayerMask mask;
    private float moveSpeed;

}
public partial class EnemyMovement : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        moveSpeed = enemy.EnemyStatInformation.move_speed;
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
public partial class EnemyMovement : MonoBehaviour // Main
{
    public void Progress()
    {
        Move();
    }
}
public partial class EnemyMovement : MonoBehaviour // Property
{
    public void SetMoveDirection()
    {
        MoveDirection = Random.Range(-1, 1);
        if (MoveDirection == 0) MoveDirection = 1;
    }
    private void Move()
    {
        if (enemy.EnemyState == EnemyState.Move)
        {

            Vector2 rayCastRoot = new Vector2(transform.position.x + (MoveDirection * 0.5f), transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(rayCastRoot, Vector2.down, 2.5f, mask);
            if (hit.collider != null)
            {
                Vector2 direction = new Vector2(MoveDirection, 0);
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
            else
                enemy.EnemyState = EnemyState.Idle;
        }
    }
}