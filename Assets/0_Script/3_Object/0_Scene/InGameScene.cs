using UnityEngine;

public partial class InGameScene : BaseScene // Data Field
{
    [SerializeField] private Player player;
    [SerializeField] private SpawnController spawnController;
}
public partial class InGameScene : BaseScene // Initialize
{
    private void Allocate()
    {

    }
    public override void Initialize()
    {
        base.Initialize();
        Allocate();
        Setup();
    }
    private void Setup()
    {
        MainSystem.Instance.PlayerManager.SignupPlayer(player);
        MainSystem.Instance.SpawnManager.SignupSpawnController(spawnController);
    }
}
public partial class InGameScene : BaseScene // 
{

}