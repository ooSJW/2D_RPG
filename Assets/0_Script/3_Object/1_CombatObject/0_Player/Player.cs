using UnityEngine;

public partial class Player : MonoBehaviour // Data Field
{
    [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerAnimation PlayerAnimation { get; private set; }
    [field: SerializeField] public PlayerGroundDetector PlayerGroundDetector { get; private set; }
}
public partial class Player : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
        PlayerInput.Initialize(this);
        PlayerMovement.Initialize(this);
        PlayerAnimation.Initialize(this);
        PlayerGroundDetector.Initialize(this);
    }
    private void Setup()
    {

    }
}
public partial class Player : MonoBehaviour // Main
{
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        PlayerMovement.FixedProgress();
    }
}