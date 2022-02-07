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

        var exec = (TurretExecutable)args["TurretExecutable"];

        yield return new WaitForSeconds(1);

        foreach (var collider in GetUnitsInArea(position, exec.Range.Value))
        {
            if (collider.TryGetComponent(out IDamageable target))
            {
                target.Damage(exec.Damage.Value, exec.ProjectileTypes);
            }
        }

        GameObject clone = Instantiate(projectileprefab);
        clone.transform.position = position;

        FindObjectOfType<RangeVisualisation>().DrawCircle(clone, exec.Range.Value, 0.1f);

    }

    private Collider2D[] GetUnitsInArea(Vector3 position, float radious)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radious);

        Debug.Log(colliders.Length);

        return colliders;

    }

}
