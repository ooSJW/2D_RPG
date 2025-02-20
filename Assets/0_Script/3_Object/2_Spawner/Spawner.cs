using UnityEngine;

public partial class Spawner : MonoBehaviour // Data Field
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private int spawnCount;
    // TODO TEST : CSV
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private float spawnTime;
    [SerializeField] private float intervalTime;
    [SerializeField] private string[] spawnableEnemyName;
    private Vector2 spawnPosition;
}
public partial class Spawner : MonoBehaviour // Initialize
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
public partial class Spawner : MonoBehaviour // 
{
    private void Update()
    {
        CheckEnemyCount();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(spawnPosition, Vector2.down * 2.5f);
    }
}
public partial class Spawner : MonoBehaviour // Property
{
    private void CheckEnemyCount()
    {
        if (spawnCount < maxEnemyCount)
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
        spawnPosition = Vector2.zero;
        spawnPosition.x = Random.Range(spawnPoints[0].transform.position.x, spawnPoints[1].transform.position.x);
        spawnPosition.y = spawnPoints[0].transform.position.y;

        /*
          RaycastHit2D ground = Physics2D.Raycast(spawnPosition, Vector2.down, 2.5f, groundMask);
          if (ground.collider != null)
          spawnPosition.y = ground.transform.position.y;
        */

        int randomEnemy = Random.Range(0, spawnableEnemyName.Length);
        Enemy enemy = MainSystem.Instance.PoolManager.Spawn(spawnableEnemyName[randomEnemy], spawnPosition).GetComponent<Enemy>();
        MainSystem.Instance.EnemyManager.SignupEnemy(enemy);
        spawnCount++;
    }

    public void DespawnEnemy(Enemy enemy)
    {
        MainSystem.Instance.EnemyManager.SigndownEnemy(enemy);
        MainSystem.Instance.PoolManager.Despawn(enemy.gameObject);
        spawnCount--;
    }
}