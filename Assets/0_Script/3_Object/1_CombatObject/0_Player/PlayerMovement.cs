using UnityEngine;

public partial class PlayerMovement : MonoBehaviour // Data Field
{
    private Player player;
    [SerializeField] private Rigidbody2D rigid;

    // TODO TEST DATA : 추후 json, csv로 작성 예정
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float airControlFactor;

    private Vector2 moveVector;
    private bool canMove;
    public bool CanMove
    {
        get => canMove;
        set
        {
            canMove = value;
        }
    }
}
public partial class PlayerMovement : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        moveVector = Vector2.zero;
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
public partial class PlayerMovement : MonoBehaviour // Main
{
    public void FixedProgress()
    {
        PlayerMove();
    }
}
public partial class PlayerMovement : MonoBehaviour // Property
{
    public void PlayerMove()
    {
        if (player.PlayerGroundDetector.IsGround)
        {
            Vector2 inputVector = player.PlayerInput.InputVector;
            moveVector.x = inputVector.x * moveSpeed;
            if (Mathf.Approximately(0, moveVector.x))
                player.PlayerState = PlayerState.Idle;
            else
                player.PlayerState = PlayerState.Run;
        }
        else
        {
            moveVector.x -= Time.fixedDeltaTime;
            player.PlayerState = PlayerState.Jump;
        }

        rigid.linearVelocity = new Vector2(moveVector.x, rigid.linearVelocityY);
    }
    public void Jump()
    {
        if (player.PlayerGroundDetector.IsGround)
        {
            rigid.linearVelocityY = jumpSpeed;
            player.PlayerGroundDetector.IsGround = false;
        }
    }
    public void EndJump()
    {
        moveVector.y = 0;
        player.PlayerState = PlayerState.Idle;
    }
    public void ApplyAirControl()
    {
        moveVector.x *= airControlFactor;
    }
}