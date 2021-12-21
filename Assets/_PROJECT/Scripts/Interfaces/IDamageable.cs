using UnityEngine;

public interface IDamageable
{

    void Damage(float damage, ProjectileType[] types);

    Player Owner();

    Vector3 Position();

}
