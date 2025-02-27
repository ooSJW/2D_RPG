using UnityEngine;

public partial class UIManager : MonoBehaviour // Data Field
{
    public UIController UIController { get; private set; }
}
public partial class UIManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class UIManager : MonoBehaviour // Sign
{
    public void SignupUIController(UIController UIControllerValue)
    {
        UIController = UIControllerValue;
        UIController.Initialize();
    }
    public void SigndownUIController()
    {
        UIController = null;
    }
}