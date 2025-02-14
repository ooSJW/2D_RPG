using UnityEngine;

public partial class PlayerGroundDetector : MonoBehaviour // Data Field
{
    private Player player;

    private bool isGround;
    public bool IsGround
    {
        get => isGround;
        set => isGround = value;
    }
}
public partial class PlayerGroundDetector : MonoBehaviour // Initialize
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
public partial class PlayerGroundDetector : MonoBehaviour // 
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            IsGround = true;
            player.PlayerMovement.EndJump();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
            IsGround = false;
    }
}