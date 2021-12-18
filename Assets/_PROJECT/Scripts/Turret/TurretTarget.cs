using UnityEngine;

public class TurretTarget : MonoBehaviour, IDamageable
{

    private Unit _unit;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    public void Damage(float damage, ProjectileType projectileType)
    {
        _unit.RemoveCurrentHealth(damage, projectileType);
    }

    public Player Owner()
    {
        return _unit.UnitOwner;
    }

    public Vector3 Position()
    {
        return transform.position;
    }
}
