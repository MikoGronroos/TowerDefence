using UnityEngine;

public class EventSetup : MonoBehaviour
{
    public void Start()
    {
        if (!EventCreator.EventsCreated)
        {
            EventCreator.CreateAllEvents();
        }
    }
}
