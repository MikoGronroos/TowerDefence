using System;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviourSingleton<CleanUp>
{

    private Dictionary<GameObject, Action> objectsToCleanUp = new Dictionary<GameObject, Action>();

    public void CleanUpScene()
    {

        foreach (var obj in objectsToCleanUp)
        {

            if (obj.Key.GetComponent<CleanUp>()) continue;

            obj.Value?.Invoke();
            Destroy(obj.Key);
        }
        objectsToCleanUp.Clear();
        objectsToCleanUp.Add(gameObject, null);
    }

    public void AddToCleanUp(GameObject obj, Action callback)
    {
        objectsToCleanUp.Add(obj, callback);
    }

}
