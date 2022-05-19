using System.Collections.Generic;
using UnityEngine;

public abstract class ExecuteJob : ScriptableObject
{

    public abstract void Job(Dictionary<string, object> args);

}