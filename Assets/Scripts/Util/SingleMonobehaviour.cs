using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMonobehaviour<T> : MonoBehaviour where T : Component
{ 
    private static T monoInstance;

    public static T Instance
    {
        get
        {
            if (monoInstance == null)
            {
                monoInstance = FindObjectOfType<T>();

                if (monoInstance == null)
                {
                    var _newGameObject = new GameObject(typeof(T).Name);
                    monoInstance = _newGameObject.GetComponent<T>();
                    DontDestroyOnLoad(_newGameObject);
                }
            }
            return monoInstance;
        }
    }

    protected virtual void Awake()
    {
        if (monoInstance == null)
        {
            monoInstance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}