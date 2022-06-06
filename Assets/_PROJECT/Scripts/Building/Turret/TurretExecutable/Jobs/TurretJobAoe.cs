using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        ProjectileSpawner.Instance.RequestProjectileSpawn(projectileprefab.name, position, new Vector2(0,0));

        SoundEffectManager.Instance.PlaySoundInstantlyWithID(shootSoundID, true);

        CollidersLogic(position, GetCollidersInArea(position, 2), exec, null);

    }

    private Collider2D[] GetCollidersInArea(Vector3 position, float radious)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radious);

        return colliders;

    }

    public virtual void CollidersLogic(Vector3 pos, Collider2D[] colliders, TurretExecutable exec, Transform parent)  {   }

}
