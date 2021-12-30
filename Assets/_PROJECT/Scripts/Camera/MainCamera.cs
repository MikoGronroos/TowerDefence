using UnityEngine;

public class MainCamera : MonoBehaviourSingletonDontDestroyOnLoad<MainCamera>
{

    public Camera ThisCamera;

    private void Awake()
    {
        ThisCamera = GetComponent<Camera>();
    }
}
