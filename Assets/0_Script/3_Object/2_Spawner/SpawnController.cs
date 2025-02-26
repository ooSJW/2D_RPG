using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class SpawnController : MonoBehaviour // Data Field
{
    [field: SerializeField] public Spawner Spawner { get; private set; }
}
public partial class SpawnController : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
        Spawner.Initialize();
    }
    private void Setup()
    {

    }
}