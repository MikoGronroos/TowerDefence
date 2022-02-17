public static class EventCreator
{

    public static bool EventsCreated = false;

    public static void CreateAllEvents()
    {
        #region Game Events

        EventManager.CreateEvent("OnUnitReachedGoal");
        EventManager.CreateEvent("OnUnitKilled");
        EventManager.CreateEvent("OnMoneyUsed");

        #endregion

        #region Settings

        EventManager.CreateEvent("OnMapChanged");

        #endregion

        EventsCreated = true;
    }

}
