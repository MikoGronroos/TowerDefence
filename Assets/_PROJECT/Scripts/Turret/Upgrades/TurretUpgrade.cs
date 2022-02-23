using UnityEngine;

[System.Serializable]
public class TurretUpgrade
{

    public string Name;

    public string Description;

    public int Price;

    public Sprite Icon;

    [Header("Turret Executable")]

    public ProjectileType[] ProjectileTypes;

    public GameObject ExecutablePrefab;

    public ExecuteJob Job;

    [Header("Value Addons")]

    public UpgradeType Type;

    public float DamageAddon;
    public float RangeAddon;
    public float AttackSpeedAddon;

    public void UseUpgrade(Turret turret)
    {

        TurretExecutable target = turret.GetTurretExecutable();

        if (ExecutablePrefab != null) target.ExecutablePrefab = ExecutablePrefab;

        if (ProjectileTypes.Length > 0) target.ProjectileTypes = ProjectileTypes;

        if (Job != null) target.Job = Job;

        switch (Type)
        {
            case UpgradeType.Percentage:
                target.Damage.BaseValue *= DamageAddon;
                target.Range.BaseValue *= RangeAddon;
                target.AttackSpeed.BaseValue *= AttackSpeedAddon;
                break;
            case UpgradeType.Whole:
                target.Damage.BaseValue += DamageAddon;
                target.Range.BaseValue += RangeAddon;
                target.AttackSpeed.BaseValue += AttackSpeedAddon;
                break;
        }
    }
}

public enum UpgradeType
{
    Percentage,
    Whole
}
