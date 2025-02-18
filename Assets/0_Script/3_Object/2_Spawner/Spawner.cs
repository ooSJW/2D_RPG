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
        float xPosition = Random.Range(spawnPoints[0].transform.position.x, spawnPoints[1].transform.position.x);
        float yPosition = 0;
        RaycastHit2D ground = Physics2D.Raycast(new Vector2(xPosition, spawnPoints[0].transform.position.y + 2.5f), Vector2.down, 5f, groundMask);
        if (ground.collider != null)
            yPosition = ground.transform.position.y;

        int randomEnemy = Random.Range(0, spawnableEnemyName.Length);
        Enemy enemy = MainSystem.Instance.PoolManager.Spawn(spawnableEnemyName[randomEnemy]).GetComponent<Enemy>();
        MainSystem.Instance.EnemyManager.SignupEnemy(enemy);
        spawnCount++;
        // TODO 
        // spawnCount 증감 어디서 할지 결정
        // enemy, spawn의 csv데이터 제작, 테스트  
    }
}