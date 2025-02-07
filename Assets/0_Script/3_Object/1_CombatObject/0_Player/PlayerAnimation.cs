using UnityEngine;

public partial class PlayerAnimation : MonoBehaviour // Data Field
{
    private Player player;
}
public partial class PlayerAnimation : MonoBehaviour // Initialize
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
public partial class PlayerAnimation : MonoBehaviour // 
{

}