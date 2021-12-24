using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour
{

    private void Awake()
    {
        EventManager.CreateEvent("OnUnitKilled");
    }


}
