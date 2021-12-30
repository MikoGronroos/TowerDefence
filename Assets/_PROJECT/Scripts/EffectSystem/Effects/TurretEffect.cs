[System.Serializable]
public class TurretEffect : Effect
{

    public TurretEffect(float addon, TurretEffectType type)
    {
        Addon = addon;
        EffectType = type;
    }

    public TurretEffectType EffectType;

}

public enum TurretEffectType
{
    Damage,
    Range,
    AttackSpeed
}
