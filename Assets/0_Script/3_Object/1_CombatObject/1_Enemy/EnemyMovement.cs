using UnityEngine;

public partial class EnemyMovement : MonoBehaviour // Data Field
{
    private Enemy enemy;
}
public partial class EnemyMovement : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize(Enemy enemyValue)
    {
        enemy = enemyValue;
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class EnemyMovement : MonoBehaviour // 
{

}