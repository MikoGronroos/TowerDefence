using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OnJoinedRoom : MonoBehaviour
{

    [SerializeField] private GameObject[] objectsToInstantiateOnStart;
    
    [SerializeField] private RoomEventChannel roomEventChannel;
    [SerializeField] private SceneManagementEventChannel sceneManagementEventChannel;

    private void Awake()
    {
        foreach (var item in objectsToInstantiateOnStart)
        {
            Instantiate(item, Vector3.zero, Quaternion.identity);
        }
    }

    private void Start()
    {
        roomEventChannel?.OnJoinedRoom(null);
    }

}
