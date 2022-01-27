using UnityEngine;

public abstract class ExecuteJob : ScriptableObject
{

    public abstract void Job(GameObject prefab, Vector3 position, Vector3 rotation, TurretExecutable exec);

}