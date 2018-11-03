using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T> {

    private static T instance;

    public static T Instance
    {
        get
        {
            if (!instance)
            {
                GameObject sm = new GameObject(typeof(T).ToString());
                sm.AddComponent<T>();
                DontDestroyOnLoad(sm);
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (!instance)
        {
            instance = (T)this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
