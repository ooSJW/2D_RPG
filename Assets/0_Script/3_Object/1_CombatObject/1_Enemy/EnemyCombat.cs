using UnityEngine;

public partial class EnemyCombat : MonoBehaviour // Data Field
{
    private Enemy enemy;

    // TODO TEST :
    //
    //
    // DATA
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask playerMask;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackDelay;
    [SerializeField] private float intervalTime;
}
public partial class EnemyCombat : MonoBehaviour // Initialize
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
public partial class EnemyCombat : MonoBehaviour // property
{
    public void CheckAttackRange()
    {
        intervalTime += Time.deltaTime;

        if (intervalTime >= attackDelay)
        {
            Vector2 targetPosition = enemy.playerTransform.position;
            float distance = Vector2.Distance(transform.position, targetPosition);
            if (distance <= attackRange)
            {
                float direction = targetPosition.x - transform.position.x;
                enemy.EnemyState = EnemyState.Attack;
                enemy.EnemyAnimation.FlipX(direction);
            }
        }
    }
    public void OnAttack()
    {
        Vector3 attackPosition = transform.position;
        attackPosition.x += Mathf.Abs(offset.x) * enemy.EnemyAnimation.GetCharacterDirection();
        attackPosition.y += offset.y;
        Collider2D hitCollider = Physics2D.OverlapBox(attackPosition, boxSize, 0, playerMask);

        if (hitCollider != null)
        {
            Player Player = hitCollider.gameObject.GetComponent<Player>();
            enemy.SendDamage(10, Player);
        }
    }

    public void EndAttack()
    {
        intervalTime = 0;
        enemy.EnemyState = EnemyState.Idle;
    }

}