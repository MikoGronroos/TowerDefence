using UnityEngine;

public class OnJoinedRoom : MonoBehaviour
{

    [SerializeField] private GameObject[] objectsToInstantiateOnStart;

    private void Awake()
    {
        foreach (var item in objectsToInstantiateOnStart)
        {
            GameObject.Instantiate(item, Vector3.zero, Quaternion.identity);
        }
    }

}
