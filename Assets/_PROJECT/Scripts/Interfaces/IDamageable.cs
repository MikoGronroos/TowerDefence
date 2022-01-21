using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{

    void Damage(float damage, IEnumerable<ProjectileType> types);

    Vector3 Position();

    public int OwnerID();

}
