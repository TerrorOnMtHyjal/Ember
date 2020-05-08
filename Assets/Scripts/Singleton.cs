using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton take #1
// Allows for any MonoBehaviour classes to be globally accessible and guarantee single instantiation
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected Singleton() {}
    private static T _instance;
    
    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                _instance = new GameObject($"({nameof(Singleton<T>)}){typeof(T)}").AddComponent<T>();
            }
            
            return _instance;
        }
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        OnAwake();
    }

    protected virtual void OnAwake() { }
}
