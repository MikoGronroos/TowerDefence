using UnityEngine;

public class TimeManager : MonoBehaviourSingletonDontDestroyOnLoad<TimeManager>
{

	[SerializeField] private TimeData currentTimeData;

    public void SaveTimeData()
    {
        SaveData.SaveTheTimeManagerData(currentTimeData);
    }

    public TimeData GetTimeData()
    {
        return currentTimeData;
    }

    public void SetTimeData(TimeData data)
    {
        currentTimeData = data;
    }

}
