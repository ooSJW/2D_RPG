using UnityEngine;
using UnityEngine.SceneManagement;

public partial class MainSystem : GenericSingleton<MainSystem> // Data Field
{
    public DataManager DataManager { get; private set; }
    public SceneManager SceneManager { get; private set; }
}
public partial class MainSystem : GenericSingleton<MainSystem> // Initialize
{
    private void Allocate()
    {
        DataManager = gameObject.AddComponent<DataManager>();
        SceneManager = gameObject.AddComponent<SceneManager>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();

        DataManager.Initialize();
        SceneManager.Initialize();
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