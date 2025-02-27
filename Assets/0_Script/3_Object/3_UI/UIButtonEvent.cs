using UnityEngine;

public partial class UIButtonEvent : MonoBehaviour // Data Field
{
    [SerializeField] private GameObject optionUI;
}
public partial class UIButtonEvent : MonoBehaviour // Initialize
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
        optionUI.SetActive(false);
    }
}
public partial class UIButtonEvent : MonoBehaviour // Property
{
    public void OnoffOptionUI()
    {
        bool isActive = optionUI.activeSelf;
        optionUI.SetActive(!isActive);
    }
    public void ChangeScene(int sceneNameValue)
    {
        SceneName sceneName = (SceneName)sceneNameValue;
        MainSystem.Instance.SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}