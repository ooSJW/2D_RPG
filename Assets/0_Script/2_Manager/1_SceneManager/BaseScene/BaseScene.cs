using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseScene : MonoBehaviour // Data Field
{
    [Header("BaseScene Member")]
    public List<GameObject> poolableObjectList;
}
public partial class BaseScene : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public virtual void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class BaseScene : MonoBehaviour // Property
{
    private void Awake()
    {
        MainSystem.Instance.SceneManager.SignupActiveScene(this);
    }
}