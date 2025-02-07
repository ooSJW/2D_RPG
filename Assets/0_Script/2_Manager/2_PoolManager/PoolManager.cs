using System.Collections.Generic;
using UnityEngine;

public partial class PoolManager : MonoBehaviour // Inner Class
{
    public partial class Pool // Data Field
    {
        private List<GameObject> poolObjectList;
        private GameObject originPrefab;
        private Transform parent;
        private int spawnCount;
    }
    public partial class Pool // Property
    {
        public Pool(GameObject originPrefabValue, int spawnCountValue = 1)
        {
            poolObjectList = new List<GameObject>();
            parent = new GameObject() { name = $"[Root] : {originPrefabValue.name}" }.transform;

            originPrefab = originPrefabValue;
            spawnCount = spawnCountValue;
        }

        public void Register()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject poolObject = Instantiate(originPrefab, parent.transform.position, Quaternion.identity, parent);
                poolObject.name = originPrefab.name;
                poolObject.SetActive(false);
                poolObjectList.Add(poolObject);
            }
        }

        public GameObject Spawn(Transform activeParent = null)
        {
            GameObject poolObject = null;
            if (poolObjectList.Count > 0)
            {
                poolObject = poolObjectList[0];
                poolObjectList.Remove(poolObject);
                poolObject.transform.SetParent(activeParent);
                poolObject.SetActive(true);
            }
            else
            {
                poolObject = Instantiate(originPrefab, activeParent.transform.position, Quaternion.identity, activeParent);
                poolObject.name = originPrefab.name;
            }
            return poolObject;
        }
        public void Despawn(GameObject poolObject)
        {
            poolObject.transform.SetParent(parent);
            poolObject.SetActive(false);
            poolObjectList.Add(poolObject);
        }
    }
}
public partial class PoolManager : MonoBehaviour // Data Field
{
    private Dictionary<string, Pool> poolDictionary;
}
public partial class PoolManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        poolDictionary = new Dictionary<string, Pool>();
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
public partial class PoolManager : MonoBehaviour // Property
{
    public void Register()
    {
        poolDictionary.Clear();
        List<GameObject> poolObjectList = MainSystem.Instance.SceneManager.ActiveScene.poolableObjectList;
        for (int i = 0; i < poolObjectList.Count; i++)
        {
            Pool pool = new Pool(poolObjectList[i]);
            pool.Register();
            poolDictionary.Add(poolObjectList[i].name, pool);
        }
    }

    public GameObject Spawn(string name)
    {
        return poolDictionary[name].Spawn();
    }
    public void Despawn(GameObject poolObject)
    {
        poolDictionary[poolObject.name].Despawn(poolObject);
    }
}