using UnityEngine;

[System.Serializable]
public class TurretExecutable
{

    public bool IsPrimaryExecutable;

    public bool NeedsTarget;

    public ProjectileType[] ProjectileTypes;

    public CustomFloat Damage;

    public CustomFloat Range;

    public CustomFloat AttackSpeed;

    public GameObject ExecutablePrefab;

    public ExecuteJob Job;

    public void Execute(Vector3 position, Vector3 rotation)
    {
        Job.Job(ExecutablePrefab, position, rotation);
    }

}
