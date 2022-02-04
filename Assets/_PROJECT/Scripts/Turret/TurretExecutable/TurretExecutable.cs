using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretExecutable
{

    public bool IsPrimaryExecutable;

    public bool NeedsTarget;

    public bool FollowsTarget;

    public ProjectileType[] ProjectileTypes;

    public CustomFloat Damage;

    public CustomFloat Range;

    public CustomFloat AttackSpeed;

    public GameObject ExecutablePrefab;

    public ExecuteJob Job;

    public void Execute(Dictionary<string, object> args)
    {

        args.Add("Prefab", ExecutablePrefab);

        Job.Job(args);
    }

}
