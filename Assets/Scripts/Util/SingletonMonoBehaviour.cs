using UnityEngine;
using System.Collections.Generic;


public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    Debug.LogError(typeof(T) + " not found");
                }
            }

            return instance;
        }
    }

    public virtual void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

}
