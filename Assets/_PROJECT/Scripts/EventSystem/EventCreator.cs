public static class EventCreator
{

    public static bool EventsCreated = false;

    public static void CreateAllEvents()
    {
        EventManager.CreateEvent("OnUnitReachedGoal");
        EventManager.CreateEvent("OnUnitKilled");
        EventManager.CreateEvent("OnMoneyUsed");
        EventsCreated = true;
    }

}
