using UnityEngine;

public partial class PlayerMovement : MonoBehaviour // Data Field
{
    private Player player;
    [SerializeField] private Rigidbody2D rigid;

    // TODO TEST DATA : 추후 json, csv로 작성 예정
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
}
public partial class PlayerMovement : MonoBehaviour // Initialize
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
public partial class PlayerMovement : MonoBehaviour // Main
{
    public void FixedProgress()
    {
        Vector2 inputVector = player.PlayerInput.InputVector;
        transform.position += (Vector3)(inputVector * moveSpeed * Time.deltaTime);
    }
}
public partial class PlayerMovement : MonoBehaviour // Property
{
    public void Jump()
    {
        if (player.PlayerInput.JumpTrigger)
        {
            if (player.PlayerGroundDetector.IsGround)
                rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }
}