using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Execute Jobs/Mortar")]
public class TurretJobMortar : ExecuteJob
{
    public override void Job(Dictionary<string, object> args)
    {

        Debug.Log("Executed");
        GameManager.Instance.StartChildCoroutine(SendMortar(args));

    }

    private IEnumerator SendMortar(Dictionary<string, object> args)
    {

        var projectileprefab = (GameObject)args["Prefab"];
        var position = (Vector3)args["TargetPosition"];

        yield return new WaitForSeconds(1);

        GameObject clone = Instantiate(projectileprefab);
        clone.transform.position = position;

    }

}
