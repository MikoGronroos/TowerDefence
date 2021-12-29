using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Turret Effect")]
public class TurretEffect : Effect
{

    public int DamageAddon;

    public override void AddEffect()
    {
        foreach (var turret in PlayerManager.Instance.GetLocalPlayer().GetPlayerTurrets())
        {
            turret.GetTurretStats().Damage += DamageAddon;
        }
    }

    public override void RemoveEffect()
    {
        foreach (var turret in PlayerManager.Instance.GetLocalPlayer().GetPlayerTurrets())
        {
            turret.GetTurretStats().Damage -= DamageAddon;
        }
    }
}
