using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Ice Effect")]
public class IceEffect : UnitEffect
{

    [field: SerializeField] public float speedSlowdownPercentage { get; private set; }

    public void init(string effectId, float speedSlowdownPercentage)
    {
        this.effectId = effectId;
        this.speedSlowdownPercentage = speedSlowdownPercentage;
    }

    public override void StartEffect(Unit unit)
    {

        unit.GetFollowPath().SetSpeed(unit.GetUnitStats().Speed / speedSlowdownPercentage);

    }

    public override void StopEffect(Unit unit)
    {
        unit.GetFollowPath().SetSpeed(unit.GetUnitStats().Speed * speedSlowdownPercentage);
    }
}
