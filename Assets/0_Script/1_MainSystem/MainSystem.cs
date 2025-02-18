using UnityEngine;
using UnityEngine.SceneManagement;

public partial class MainSystem : GenericSingleton<MainSystem> // Data Field
{
    public DataManager DataManager { get; private set; }
    public SceneManager SceneManager { get; private set; }
    public PoolManager PoolManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public EnemyManager EnemyManager { get; private set; }
    public SpawnManager SpawnManager { get; private set; }
}
public partial class MainSystem : GenericSingleton<MainSystem> // Initialize
{
    private void Allocate()
    {
        DataManager = gameObject.AddComponent<DataManager>();
        SceneManager = gameObject.AddComponent<SceneManager>();
        PoolManager = gameObject.AddComponent<PoolManager>();
        PlayerManager = gameObject.AddComponent<PlayerManager>();
        EnemyManager = gameObject.AddComponent<EnemyManager>();
        SpawnManager = gameObject.AddComponent<SpawnManager>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();

        DataManager.Initialize();
        SceneManager.Initialize();
        PoolManager.Initialize();
        PlayerManager.Initialize();
        EnemyManager.Initialize();
        SpawnManager.Initialize();
    }
    private void Setup()
    {

    }
}
public partial class MainSystem : GenericSingleton<MainSystem> // Property
{
    public void MainSystemStart()
    {
        Initialize();
        SceneManager.LoadScene("InGameScene");
    }
}