using UnityEngine;

public partial class PlayerInput : MonoBehaviour // Data Field
{
    private Player player;
    private PlayerInputAction playerInputAction;

}
public partial class PlayerInput : MonoBehaviour // Data Property
{
    private Vector2 inputVector;
    public Vector2 InputVector
    {
        get
        {
            SetInputValue();
            return inputVector;
        }
        private set { inputVector = value; }
    }

    private bool jumpTrigger;
    public bool JumpTrigger
    {
        get => jumpTrigger;
        private set
        {
            jumpTrigger = value;
            if (jumpTrigger)
            {
                inputVector.y = 1;
                player.PlayerMovement.Jump();
            }
            else
                inputVector.y = 0;
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

        playerInputAction.PlayerInput.Jump.performed += ctx => JumpTrigger = true;
        playerInputAction.PlayerInput.Jump.canceled += ctx => JumpTrigger = false;
    }

    public void SetInputValue()
    {
        bool isLeftPressed = playerInputAction.PlayerInput.Left.IsPressed();
        bool isRightPressed = playerInputAction.PlayerInput.Right.IsPressed();
        int inputX = 0;
        if (isLeftPressed && isRightPressed)
            inputX = 0;
        else if (isLeftPressed)
            inputX = -1;
        else if (isRightPressed)
            inputX = 1;
        else
            inputX = 0;

        inputVector.x = inputX;
    }
    public void InputAbleSetting(bool value)
    {
        if (value)
            playerInputAction.Enable();
        else
            playerInputAction.Disable();
    }
    public bool IsAttackPressed()
    {
        return playerInputAction.PlayerInput.Attack.IsPressed();
    }
}
