using UnityEngine;

public partial class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour// Data Field
{
    private static T instance;
    public static T Instance { get { Initialize(); return instance; } }
}
public partial class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour // Property
{
    private static void Initialize()
    {
        if (instance == null)
        {
            instance = FindFirstObjectByType<T>();
            if (instance == null)
            {
                GameObject gameObject = new GameObject() { name = $"[Singleton] : {typeof(T)}" };
                instance = gameObject.AddComponent<T>();
                DontDestroyOnLoad(instance.gameObject);
            }
        }
    }
}
