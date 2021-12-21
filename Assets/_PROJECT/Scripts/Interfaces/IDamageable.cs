using UnityEngine;

public interface IDamageable
{

    void Damage(float damage, ProjectileType[] types);

    Vector3 Position();

    public int OwnerID();

}
