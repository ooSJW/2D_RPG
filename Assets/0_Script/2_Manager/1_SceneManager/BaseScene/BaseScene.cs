using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public partial class BaseScene : MonoBehaviour // Data Field
{
    [Header("BaseScene Member")]
    public List<GameObject> poolableObjectList;

    private SceneName sceneName;
    public SceneName SceneName { get => sceneName; }
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
        SceneName[] NotCombatScene = { SceneName.InitializeScene, SceneName.LoadingScene, SceneName.LoadingScene };
        SceneName currentSceneName = NotCombatScene.FirstOrDefault(elem => elem == SceneName);
        if (currentSceneName == SceneName.None)
        {
            MainSystem.Instance.PoolManager.Register();
            EnemyManager enemyManager = MainSystem.Instance.EnemyManager;
            if (enemyManager != null)
                enemyManager.CurrentSceneName = sceneName;
        }
    }
}
public partial class BaseScene : MonoBehaviour // Property
{
    private void Awake()
    {
        MainSystem.Instance.SceneManager.SignupActiveScene(this);
    }
}