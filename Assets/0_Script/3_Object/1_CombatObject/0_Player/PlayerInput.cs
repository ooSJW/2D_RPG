using UnityEngine;

public partial class PlayerInput : MonoBehaviour // Data Field
{
    private Player player;
    private PlayerInputAction playerInputAction;

}
public partial class PlayerInput : MonoBehaviour // Data Property
{
    private Vector2 inputVector;
    public Vector2 InputVector { get => inputVector; private set { inputVector = value; } }

    private bool jumpTrigger;
    public bool JumpTrigger
    {
        get => jumpTrigger;
        private set
        {
            jumpTrigger = value;
            player.PlayerMovement.Jump();
        }
    }


}

public partial class PlayerInput : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        jumpTrigger = false;
        inputVector = Vector2.zero;

        InputActionInitialize();
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
public partial class PlayerInput : MonoBehaviour // Private Property
{
    private void InputActionInitialize()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();

        playerInputAction.PlayerInput.Left.performed += ctx => inputVector.x = -1;
        playerInputAction.PlayerInput.Left.canceled += ctx => inputVector.x = 0;

        playerInputAction.PlayerInput.Right.performed += ctx => inputVector.x = 1;
        playerInputAction.PlayerInput.Right.canceled += ctx => inputVector.x = 0;

        playerInputAction.PlayerInput.Jump.performed += ctx => JumpTrigger = true;
        playerInputAction.PlayerInput.Jump.canceled += ctx => JumpTrigger = false;
    }


}