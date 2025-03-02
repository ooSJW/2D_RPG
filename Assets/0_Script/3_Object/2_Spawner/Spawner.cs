using UnityEngine;

public partial class Spawner : MonoBehaviour // Data Field
{
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int maxSpawnCount;
    [SerializeField] private float spawnTime;
    [SerializeField] private float intervalTime;
    [SerializeField] private string[] spawnableEnemy;
    private Vector2 spawnPosition;
}
public partial class Spawner : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        spawnTime = MainSystem.Instance.EnemyManager.SpawnInterval;
        maxSpawnCount = MainSystem.Instance.EnemyManager.MaxSpawnCount;
        spawnableEnemy = MainSystem.Instance.EnemyManager.SpawnableEnemy;
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {
        SpawnEnemy();
    }
}
public partial class Spawner : MonoBehaviour // 
{
    private void Update()
    {
        CheckEnemyCount();
    }
}
public partial class Spawner : MonoBehaviour // Property
{
    private void CheckEnemyCount()
    {
        if (MainSystem.Instance.EnemyManager.EnemyList.Count < maxSpawnCount)
            SpawnTimer();
    }
    private void SpawnTimer()
    {
        intervalTime += Time.deltaTime;
        if (intervalTime >= spawnTime)
        {
            SpawnEnemy();
            intervalTime = 0;
        }
    }

    private void SpawnEnemy()
    {
        for (int i = MainSystem.Instance.EnemyManager.EnemyList.Count; i < maxSpawnCount; i++)
        {
            int randomPoint = Random.Range(0, spawnPoints.Length);
            int randomEnemy = Random.Range(0, spawnableEnemy.Length);
            spawnPosition = spawnPoints[randomPoint].transform.position;
            Enemy enemy = MainSystem.Instance.PoolManager.Spawn(spawnableEnemy[randomEnemy], spawnPosition).GetComponent<Enemy>();
            MainSystem.Instance.EnemyManager.SignupEnemy(enemy);
        }
    }
}