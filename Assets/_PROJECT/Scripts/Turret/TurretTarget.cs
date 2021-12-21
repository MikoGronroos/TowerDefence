using UnityEngine;

public class TurretTarget : MonoBehaviour, IDamageable
{

    private Unit _unit;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    public void Damage(float damage, ProjectileType[] projectileTypes)
    {
        _unit.RemoveCurrentHealth(damage, projectileTypes);
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
