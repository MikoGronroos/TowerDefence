using UnityEngine;

[System.Serializable]
public class TurretUpgrade
{

    public string Name;

    public string Description;

    public int Price;

    public Sprite Icon;

    [Header("Turret Executable")]

    public bool ChangeTurretExecutable;

    public TurretExecutable TurretExecutableAddon;

    [Header("Value Addons")]

    public UpgradeType Type;

    public float DamageAddon;
    public float RangeAddon;
    public float AttackSpeedAddon;

    public void UseUpgrade(Turret turret)
    {

        if (ChangeTurretExecutable)
        {
            turret.AddNewTurretExecutable(TurretExecutableAddon);
        }

        TurretExecutable target = turret.GetTurretExecutable();

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
