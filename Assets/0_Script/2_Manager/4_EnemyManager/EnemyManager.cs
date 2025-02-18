using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyManager : MonoBehaviour // Data Field
{
    public List<Enemy> EnemyList { get; private set; }
}
public partial class EnemyManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        EnemyList = new List<Enemy>();
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