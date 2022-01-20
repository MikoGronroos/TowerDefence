using UnityEngine;

[System.Serializable]
public class TurretUpgrade
{

    public string Name;

    public string Description;

    public int Price;

    public Sprite Icon;

    public TurretExecutable[] TurretExecutableAddons;

    public UpgradeType Type;
    public UpgradeTarget Target;

    [Header("Value Addons")]
    public float DamageAddon;
    public float RangeAddon;
    public float AttackSpeedAddon;

    public void UseUpgrade(Turret turret)
    {
        if (TurretExecutableAddons.Length > 0)
        {
            foreach (var addon in TurretExecutableAddons)
            {
                if (addon.IsPrimaryExecutable)
                {
                    turret.AddNewPrimaryExecutable(addon);
                }
                else
                {
                    turret.AddNewSecondaryExecutable(addon);
                }
            }
        }

        TurretExecutable target = DetermineTarget(turret);

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

    private TurretExecutable DetermineTarget(Turret turret)
    {
        TurretExecutable target = null;
        switch (Target)
        {
            case UpgradeTarget.Primary:
                target = turret.GetPrimaryTurretExecutable();
                break;
            case UpgradeTarget.Secondary:
                target = turret.GetSecondaryTurretExecutable();
                break;
        }
        return target;
    }

}

public enum UpgradeType
{
    Percentage,
    Whole
}

public enum UpgradeTarget
{
    Primary,
    Secondary
}
