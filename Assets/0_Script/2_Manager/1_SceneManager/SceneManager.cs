using UnityEngine;

public partial class SceneManager : MonoBehaviour // Data Field
{
    public BaseScene ActiveScene { get; private set; }
}
public partial class SceneManager : MonoBehaviour // Initialize
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
public partial class SceneManager : MonoBehaviour // Property
{
    public void SignupActiveScene(BaseScene activeSceneValue)
    {
        ActiveScene = activeSceneValue;
        ActiveScene.Initialize();
    }
    public void SigndownActiveScene()
    {
        ActiveScene = null;
    }
}
public partial class SceneManager : MonoBehaviour // Property
{
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}