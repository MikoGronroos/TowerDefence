using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(menuName = "Execute Jobs/Basic Shoot")]
public class TurretJobBasicShoot : ExecuteJob
{

    [Header("Turret variables")]
    [SerializeField] private int amountOfShots;
    [SerializeField] private float shotInterval;

    public override void Job(Dictionary<string, object> args)
    {

        var prefab = (GameObject)args["Prefab"];
        var position = (Vector3)args["Position"];

        var rotation = (Vector2)args["Rotation"];
        var exec = (TurretExecutable)args["TurretExecutable"];

        var shootSoundID = (string)args["ShootSoundID"];


        CoroutineCaller.Instance.StartChildCoroutine(Shoot(amountOfShots, shotInterval, prefab, position, rotation, exec, shootSoundID));

    }

    private IEnumerator Shoot(int amountOfShots, float time, GameObject prefab, Vector3 position, Vector2 rotation, TurretExecutable exec, string shootSoundID)
    {

        for (int i = 0; i < amountOfShots; i++)
        {
            SoundEffectManager.Instance.PlaySoundInstantlyWithID(shootSoundID, true);
            GameObject clone = PhotonNetwork.Instantiate($"Projectiles/{prefab.name}", position, Quaternion.identity);
            var projectile = clone.GetComponent<Projectile>();
            projectile.Setup(rotation, exec);

            yield return new WaitForSeconds(time);
        }

    }

}
