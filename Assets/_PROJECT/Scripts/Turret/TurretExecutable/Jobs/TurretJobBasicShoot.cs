using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(menuName = "Execute Jobs/Basic Shoot")]
public class TurretJobBasicShoot : ExecuteJob
{
    public override void Job(Dictionary<string, object> args)
    {

        var prefab = (GameObject)args["Prefab"];
        var position = (Vector3)args["Position"];

        var rotation = (Vector2)args["Rotation"];
        var exec = (TurretExecutable)args["TurretExecutable"];

        GameObject clone = PhotonNetwork.Instantiate($"Projectiles/{prefab.name}", position, Quaternion.identity);
        var projectile = clone.GetComponent<Projectile>();
        projectile.Setup(rotation, exec);
    }
}
