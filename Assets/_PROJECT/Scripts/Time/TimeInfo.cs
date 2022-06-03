using System;
using UnityEngine;

[Serializable]
public class TimeInfo
{

    public TimeInfo(DateTime time)
    {
        Year = time.Year;
        Month = time.Month;
        Day = time.Day;
        Hour = time.Hour;
        Minute = time.Minute;
        Second = time.Second;
    }

    [SerializeField] private int Year;
    [SerializeField] private int Month;
    [SerializeField] private int Day;
    [SerializeField] private int Hour;
    [SerializeField] private int Minute;
    [SerializeField] private int Second;

    public DateTime GetTimeInfoAsDateTime()
    {
        return new DateTime(Year, Month, Day, Hour, Minute, Second);
    }

    public bool IsEmpty()
    {
        return Year == 0 && Month == 0 && Day == 0 && Hour == 0 && Minute == 0 && Second == 0;
    }

}
