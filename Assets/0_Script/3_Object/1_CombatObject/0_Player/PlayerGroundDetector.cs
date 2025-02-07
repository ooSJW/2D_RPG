using UnityEngine;

public partial class PlayerGroundDetector : MonoBehaviour // Data Field
{
    private Player player;

    private bool isGround;
    public bool IsGround { get => isGround; private set => isGround = value; }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            IsGround = true;
        // TODO : 점프 버그 처리 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            IsGround = false;
    }
}