using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class TurretJobAoe : ExecuteJob
{
    public override void Job(Dictionary<string, object> args)
    {

        Debug.Log("Executed");
        GameManager.Instance.StartChildCoroutine(MakeAoe(args));

    }

    private IEnumerator MakeAoe(Dictionary<string, object> args)
    {

        var projectileprefab = (GameObject)args["Prefab"];
        var position = (Vector3)args["TargetPosition"];

        var exec = (TurretExecutable)args["TurretExecutable"];

        yield return new WaitForSeconds(1);

        CollidersLogic(GetCollidersInArea(position, exec.Range.Value), exec);

        GameObject clone = PhotonNetwork.Instantiate($"Projectiles/{projectileprefab.name}", position, Quaternion.identity);

        FindObjectOfType<RangeVisualisation>().DrawCircle(clone, exec.Range.Value, 0.1f);

    }

    private Collider2D[] GetCollidersInArea(Vector3 position, float radious)
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radious);

        return colliders;

    }

    public virtual void CollidersLogic(Collider2D[] colliders, TurretExecutable exec)  {   }

}
