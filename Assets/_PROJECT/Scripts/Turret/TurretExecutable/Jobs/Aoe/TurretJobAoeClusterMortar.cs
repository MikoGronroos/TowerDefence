using UnityEngine;

[CreateAssetMenu(menuName = "Execute Jobs/TurretJobAoeClusterMortar", fileName = "TurretJobAoeClusterMortar")]
public class TurretJobAoeClusterMortar : TurretJobAoe
{

    [SerializeField] private float ricochetDistance;

    public override void CollidersLogic(Vector3 pos, Collider2D[] colliders, TurretExecutable exec)
    {

        Vector3[] ricochetPositions = { 
            pos + (new Vector3(1,0,0) * ricochetDistance),
            pos + (new Vector3(-1,0,0) * ricochetDistance), 
            pos + (new Vector3(0,1,0) * ricochetDistance), 
            pos + (new Vector3(0,-1,0) * ricochetDistance) 
        };

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable target))
            {
                target.Damage(exec.Damage.Value, exec.ProjectileTypes);
            }
        }

        foreach (var position in ricochetPositions)
        {
            GameObject clone = new GameObject();
            clone.transform.position = position;
            FindObjectOfType<RangeVisualisation>().DrawCircle(clone, 1, 0.1f);
        }

    }
}
