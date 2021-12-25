public static class EventCreator
{

    public static void CreateAllEvents()
    {
        EventManager.CreateEvent("OnUnitReachedGoal");
        EventManager.CreateEvent("OnUnitKilled");
        EventManager.CreateEvent("OnMoneyUsed");
    }

}
