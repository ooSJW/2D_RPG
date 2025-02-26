using UnityEngine;

public partial class EnemyCombat : MonoBehaviour // Data Field
{
    private Enemy enemy;
    private Vector2 offset;
    private Vector2 boxSize;

    [SerializeField] private LayerMask playerMask;

    private float attackRange;
    private float attackDelay;
    private float intervalTime;
}
public partial class EnemyCombat : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        boxSize = new Vector2(enemy.EnemyStatInformation.attack_range[0], enemy.EnemyStatInformation.attack_range[1]);
        offset = new Vector2(enemy.EnemyStatInformation.attack_offset[0], enemy.EnemyStatInformation.attack_offset[1]);
        attackRange = boxSize.x + offset.x;
        attackDelay = enemy.EnemyStatInformation.attack_delay;
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
            if (MainSystem.Instance.PlayerManager.PlayerAlive())
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
            enemy.SendDamage(enemy.EnemyStatInformation, Player);
        }
    }

    public void EndAttack()
    {
        intervalTime = 0;
        enemy.EnemyState = EnemyState.Idle;
    }

}