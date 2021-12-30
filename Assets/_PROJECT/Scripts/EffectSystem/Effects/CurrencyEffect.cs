using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Currency Effect")]
public class CurrencyEffect : Effect
{

    public CurrencyEffect(float addon, CurrencyEffectType type)
    {
        Addon = addon;
        EffectType = type;
    }

    public CurrencyEffectType EffectType;

}

public enum CurrencyEffectType
{
    Income
}
