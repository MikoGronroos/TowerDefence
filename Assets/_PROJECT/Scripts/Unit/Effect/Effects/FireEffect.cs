using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Fire Effect")]
public class FireEffect : UnitEffect
{

    [field: SerializeField] public float damageAmount { get; private set; }
    [field: SerializeField] public float damageInterval { get; private set; }
    [field: SerializeField] public ProjectileType damageType { get; private set; }

    private float _timeLeft;

    public override void StartEffect(Unit unit)
    {
        _timeLeft = damageInterval;
    }

    public override void UpdateEffect(Unit unit)
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
        }
        else
        {
            unit.RemoveCurrentHealth(damageAmount, new ProjectileType[1] { damageType });
            _timeLeft = damageInterval;
        }
    }

    public override void StopEffect(Unit unit)
    {

    }

}
