using Finark.Events;
using UnityEngine;
using System.Collections.Generic;

public class AssignCamera : MonoBehaviour
{

    [SerializeField] private CameraEventChannel cameraEventChannel;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Start()
    { 
        
        cameraEventChannel.AssignCameraToCanvas(null, CameraCallback);

    }

    private void CameraCallback(Dictionary<string, object> args)
    {

        var camera = (Camera)args["Camera"];

        _canvas.worldCamera = camera;

    }
}
