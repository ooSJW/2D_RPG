using UnityEngine;

public partial class UIController : MonoBehaviour // Data Field
{
    [field: SerializeField] public UIButtonEvent UIButtonEvent { get; private set; }
}
public partial class UIController : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
        UIButtonEvent.Initialize();
    }
    private void Setup()
    {

    }
}
public partial class UIController : MonoBehaviour // 
{

}