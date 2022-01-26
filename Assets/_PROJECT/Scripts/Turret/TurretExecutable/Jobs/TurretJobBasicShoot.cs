using UnityEngine;

[CreateAssetMenu(menuName = "Execute Jobs/Basic Shoot")]
public class TurretJobBasicShoot : ExecuteJob
{
    public override void Job(GameObject prefab, Vector3 position, Vector3 rotation)
    {
        GameObject clone = Instantiate(prefab);
        clone.transform.position = position;
        var projectile = clone.GetComponent<Projectile>();
        projectile.Setup(rotation);
    }
}
