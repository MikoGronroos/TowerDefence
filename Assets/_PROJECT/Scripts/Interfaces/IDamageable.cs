using UnityEngine;

public interface IDamageable
{

    void Damage(float damage, ProjectileType type);

    Player Owner();

    Vector3 Position();

}
