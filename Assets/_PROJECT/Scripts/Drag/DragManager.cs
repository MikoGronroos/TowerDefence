using UnityEngine;

public class DragManager : MonoBehaviourSingleton<DragManager>
{

    [SerializeField] private GameObject dragObject;

    public GameObject GetDragObject()
    {
        return dragObject;
    }

}
