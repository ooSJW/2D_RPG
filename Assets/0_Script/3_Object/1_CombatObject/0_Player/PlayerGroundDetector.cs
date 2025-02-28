using UnityEngine;

public partial class PlayerGroundDetector : MonoBehaviour // Data Field
{
    private Player player;

    private bool isGround;
    public bool IsGround
    {
        get => isGround;
        set
        {
            if (isGround != value)
            {
                isGround = value;
                if (isGround)
                {
                    player.PlayerMovement.EndJump();
                    groundCollider.enabled = true;
                }
                else
                    groundCollider.enabled = false;
            }
        }
    }

    // TODO TEST
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float groundCheckLength;
    [SerializeField] private Collider2D groundCollider;
    private bool[] raycastCheck;
    private bool overlabCheck;

}
public partial class PlayerGroundDetector : MonoBehaviour // Initialize
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 startPos = transform.position;

        startPos.x -= 0.4f;
        Gizmos.DrawLine(startPos, startPos + Vector2.down * groundCheckLength);
        startPos.x += 0.7f;
        Gizmos.DrawLine(startPos, startPos + Vector2.down * groundCheckLength);

        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }
    private void Allocate()
    {
        raycastCheck = new bool[2] { true, true };
        overlabCheck = true;
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
public partial class PlayerGroundDetector : MonoBehaviour // 
{
    public void Progress()
    {
        GroundCheck();
    }
    private void GroundCheck()
    { 
        // TODO : 정상적으로 보임 , 문제는 점프했을 때 타일맵 안에 플레이어 GroundCollider가 갇히는 버그가있음.
        if (player.PlayerMovement.GetVelocityY() < 0)
        {
            Vector2 raycastPosition = transform.position;
            raycastPosition.x -= 0.4f;
            raycastCheck[0] = Physics2D.Raycast(raycastPosition, Vector2.down, groundCheckLength, groundLayer);

            raycastPosition.x += 0.7f;
            raycastCheck[1] = Physics2D.Raycast(raycastPosition, Vector2.down, groundCheckLength, groundLayer);
            overlabCheck = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);

            if (raycastCheck[0] || raycastCheck[1] || overlabCheck)
                IsGround = true;
            else
                IsGround = false;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Ground"))
    //    {
    //        IsGround = true;
    //        player.PlayerMovement.EndJump();
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("Ground"))
    //        IsGround = false;
    //}
}