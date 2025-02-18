using UnityEngine;

public partial class EnemyAnimation : MonoBehaviour // Data Field
{
    private Enemy enemy;
}
public partial class EnemyAnimation : MonoBehaviour // Initialize
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
public partial class EnemyAnimation : MonoBehaviour // 
{

}