using UnityEngine;

[CreateAssetMenu(menuName = "Unit Stats")]
public class UnitStats : ScriptableObject
{

    public float StartHealth;

    public float Speed;

    public int Damage;

    public ProjectileType[] ProjectileTypesAllowed;

    public int XpAddonOnDestroyed;

}
