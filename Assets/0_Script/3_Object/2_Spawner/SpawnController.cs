using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class SpawnController : MonoBehaviour // Data Field
{
    [field: SerializeField] public List<Spawner> SpawnerList { get; private set; }
}
public partial class SpawnController : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        SpawnerList = new List<Spawner>();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        for (int i = 0; i < SpawnerList.Count; i++)
            SpawnerList[i].Initialize();
    }
    private void Setup()
    {

    }
}