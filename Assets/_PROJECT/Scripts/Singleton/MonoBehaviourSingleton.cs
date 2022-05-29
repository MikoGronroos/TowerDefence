using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour
{

    private static object _instance;

    public static T Instance
    {
        get
        {
            return (T)_instance;
        }
    }

    public virtual void OnEnable()
    {

        _instance = this;

    }

}