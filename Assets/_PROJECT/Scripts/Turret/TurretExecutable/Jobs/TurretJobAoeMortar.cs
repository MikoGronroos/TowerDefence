using UnityEngine;

[CreateAssetMenu(menuName = "Execute Jobs/Mortar")]
public class TurretJobAoeMortar : TurretJobAoe
{
    public override void CollidersLogic(Collider2D[] colliders, TurretExecutable exec)
    {
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable target))
            {
                target.Damage(exec.Damage.Value, exec.ProjectileTypes);
            }
        }
    }
}