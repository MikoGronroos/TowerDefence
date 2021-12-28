using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviourSingleton<CleanUp>
{

    [SerializeField] private List<GameObject> objectsToCleanUp = new List<GameObject>();

    public void CleanUpScene()
    {
        foreach (var obj in objectsToCleanUp)
        {

            if (obj.GetComponent<CleanUp>()) continue;

            Destroy(obj);
        }
        objectsToCleanUp.Clear();
        objectsToCleanUp.Add(gameObject);
    }

    public void AddToCleanUp(GameObject obj)
    {
        objectsToCleanUp.Add(obj);
    }

}
