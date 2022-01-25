using UnityEngine;

[CreateAssetMenu(menuName = "Execute Jobs/Basic Shoot")]
public class TurretJobBasicShoot : ExecuteJob
{
    public override void Job(GameObject prefab)
    {
        GameObject clone = Instantiate(prefab);
    }
}
