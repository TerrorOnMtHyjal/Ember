using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Allows for any MonoBehaviour classes to be single instantiation and globally accessible
public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
    protected Singleton() {}
    private static T _instance;
    
    public static T Instance
    {
        get
        {
            if (Quitting) return null;
            if (_instance != null) return _instance;
            
            var instances = FindObjectsOfType<T>();
            var count = instances.Length;
            
            if (count == 1) return _instance = instances[0];
            if (count >= 2)
            {
                _instance = instances[0];
                Debug.LogWarning($"[{nameof(Singleton<T>)}<{typeof(T)}>] Attempted instantiation of {nameof(Singleton<T>)} of type {typeof(T)} in the scene when {count} were found. Returning first found and destroying others.");
                instances.ToList().ForEach(Destroy);

                return _instance;
            }
            
            Debug.Log($"[{nameof(Singleton<T>)}<{typeof(T)}>] An instance is needed in the scene and none were found, a new instance will be created.");
            _instance = new GameObject($"({nameof(Singleton<T>)}){typeof(T)}").AddComponent<T>();
            
            return _instance;
        }
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        OnAwake();
    }

    protected virtual void OnAwake() {}
}

public abstract class Singleton : MonoBehaviour
{
    protected static bool Quitting { get; private set; }
    
    private void OnApplicationQuit()
    {
        Quitting = true;
    }
}
