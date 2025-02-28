using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static EnemyManagerData;

public partial class EnemyManager : MonoBehaviour // Data Property
{
    public List<Enemy> EnemyList { get; private set; }
    public int MaxSpawnCount { get; private set; }
    public float SpawnInterval { get; private set; }
    public string[] SpawnableEnemy { get; private set; }

    private EnemyManagerInformation enemyManagerInformation;
    public EnemyManagerInformation EnemyManagerInformation
    {
        get => enemyManagerInformation;
        private set
        {
            enemyManagerInformation = new EnemyManagerInformation()
            {
                index = value.index,
                scene_name = value.scene_name,
                max_spawn_count = value.max_spawn_count,
                spawn_time = value.spawn_time,
                spawnable_enemy = value.spawnable_enemy,
            };
            MaxSpawnCount = value.max_spawn_count;
            SpawnInterval = value.spawn_time;
            SpawnableEnemy = value.spawnable_enemy;
        }
    }

    private SceneName currentSceneName;
    public SceneName CurrentSceneName
    {
        get => currentSceneName;
        set
        {
            currentSceneName = value;
            EnemyManagerInformation = MainSystem.Instance.DataManager.EnemyManagerData.GetData(currentSceneName);
        }
    }
}
public partial class EnemyManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        EnemyList = new List<Enemy>();
        currentSceneName = SceneName.None;
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

public partial class EnemyManager : MonoBehaviour // Sign
{
    public void SignupEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
        enemy.Initialize();
    }

    public void SigndownEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
    }
}