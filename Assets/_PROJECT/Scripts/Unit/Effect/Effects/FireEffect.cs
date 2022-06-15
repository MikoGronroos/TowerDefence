using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Fire Effect")]
public class FireEffect : UnitEffect
{

    [field: SerializeField] public float damagePerSecond { get; private set; }

    public void init(string effectId, float damagePerSecond)
    {
        this.effectId = effectId;
        this.damagePerSecond = damagePerSecond;
    }

    public override void StartEffect(Unit unit)
    {
    }

    public override void StopEffect(Unit unit)
    {
    }
}
