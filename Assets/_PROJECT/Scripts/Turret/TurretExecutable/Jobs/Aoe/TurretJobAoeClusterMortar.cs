using UnityEngine;

[CreateAssetMenu(menuName = "Execute Jobs/TurretJobAoeClusterMortar", fileName = "TurretJobAoeClusterMortar")]
public class TurretJobAoeClusterMortar : TurretJobAoe
{

    [SerializeField] private float ricochetDistance;

    public override void CollidersLogic(Vector3 pos, Collider2D[] colliders, TurretExecutable exec, Transform parent)
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

            clone.transform.SetParent(parent);

            Collider2D[] clusterColliders = Physics2D.OverlapCircleAll(position, 1);

            foreach (var col in clusterColliders)
            {
                if (col.TryGetComponent(out IDamageable target))
                {
                    //Divide the main damage by 2
                    target.Damage(exec.Damage.Value / 2, exec.ProjectileTypes);
                }
            }

            //Debug visuals change for the demo
            FindObjectOfType<RangeVisualisation>().DrawCircle(clone, 1, 0.1f);
        }

    }
}
