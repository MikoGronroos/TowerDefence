using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    [SerializeField] private Camera _thisCamera;

    [SerializeField] private CameraEventChannel cameraEventChannel;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        cameraEventChannel.AssignCameraToCanvas += CameraAssignCallback;
    }

    private void OnDisable()
    {
        cameraEventChannel.AssignCameraToCanvas -= CameraAssignCallback;
    }

    private void CameraAssignCallback(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        callback?.Invoke(new Dictionary<string, object> { { "Camera", _thisCamera } });
    }

}
