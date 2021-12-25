
using System.Collections.Generic;
using UnityEngine;

public class EventTester : MonoBehaviour
{

    public void ButtonPress()
    {
        EventManager.SubscribeToEvent("OnUnitReachedGoal", OnUnitReachedGoal);
    }

    public void OnDisable()
    {
        EventManager.UnsubscribeToEvent("OnUnitReachedGoal", OnUnitReachedGoal);
    }


    void OnUnitReachedGoal(Dictionary<string, object> message)
    {
        int unitID = (int)message["UnitID"];
        int goalOwnerID = (int)message["GoalOwnerID"];
        Debug.Log($"{unitID}:{goalOwnerID}");
    }

}
