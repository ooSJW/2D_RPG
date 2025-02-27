using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public partial class BaseScene : MonoBehaviour // Data Field
{
    [Header("BaseScene Member")]

    private SceneName sceneName;
    public SceneName SceneName { get => sceneName; }
    [SerializeField] private UIController uiController;

    [Header("CombatScene Member")]
    public List<GameObject> poolableObjectList;
    [SerializeField] private Player player;
    [SerializeField] private SpawnController spawnController;
}
public partial class BaseScene : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        if (!Enum.TryParse<SceneName>(name, out sceneName))
            print($"Parse Error SceneName[{name}]");
    }
    public virtual void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

        SceneName[] initializeScene = { SceneName.InitializeScene, SceneName.LoadingScene };
        SceneName[] notCombatScene = { SceneName.LobbyScene };
        SceneName currentSceneName = initializeScene.FirstOrDefault(elem => elem == SceneName);

        if (currentSceneName != SceneName.None) // currentScene == InitializeScene
        {
            return;
        }

        currentSceneName = notCombatScene.FirstOrDefault(elem => elem == SceneName);

        if (currentSceneName != SceneName.None) // currentScene == NotCombatScene
        {

        }
        else // currentScene == CombatScene
        {
            MainSystem.Instance.PoolManager.Register();
            EnemyManager enemyManager = MainSystem.Instance.EnemyManager;
            if (enemyManager != null)
                enemyManager.CurrentSceneName = sceneName;

            MainSystem.Instance.PlayerManager.SignupPlayer(player);
            MainSystem.Instance.SpawnManager.SignupSpawnController(spawnController);
        }
        MainSystem.Instance.UIManager.SignupUIController(uiController);
    }
}
public partial class BaseScene : MonoBehaviour // Property
{
    private void Awake()
    {
        MainSystem.Instance.SceneManager.SignupActiveScene(this);
    }
}