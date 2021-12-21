using UnityEngine;

[CreateAssetMenu(menuName = "Turret Stats")]
public class TurretStats : ScriptableObject
{

    public string Name;

    public float Damage;

    public float Range;

    public float AttackSpeed;

    public ProjectileType[] Projectiles;


}
