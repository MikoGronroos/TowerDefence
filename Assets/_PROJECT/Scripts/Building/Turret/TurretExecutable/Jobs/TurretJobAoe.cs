using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class TurretJobAoe : ExecuteJob
{
    public override void Job(Dictionary<string, object> args)
    {

        CoroutineCaller.Instance.StartChildCoroutine(MakeAoe(args));

    }

    private IEnumerator MakeAoe(Dictionary<string, object> args)
    {

        var projectileprefab = (GameObject)args["Prefab"];
        var position = (Vector3)args["TargetPosition"];

        var exec = (TurretExecutable)args["TurretExecutable"];
        var shootSoundID = (string)args["ShootSoundID"];

        yield return new WaitForSeconds(1);

        GameObject clone = PhotonNetwork.Instantiate($"Projectiles/{projectileprefab.name}", position, Quaternion.identity);

        SoundEffectManager.Instance.PlaySoundInstantlyWithID(shootSoundID, true);

        CollidersLogic(position, GetCollidersInArea(position, 2), exec, clone.transform);

        RangeVisualisation.Instance.DrawCircle(clone, 2, 0.1f);

    }

    private Collider2D[] GetCollidersInArea(Vector3 position, float radious)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radious);

        return colliders;

    }

    public virtual void CollidersLogic(Vector3 pos, Collider2D[] colliders, TurretExecutable exec, Transform parent)  {   }

}
