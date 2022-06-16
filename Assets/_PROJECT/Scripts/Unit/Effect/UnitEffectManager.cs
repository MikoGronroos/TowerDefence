using Photon.Pun;
using System.Collections;
using UnityEngine;

public class UnitEffectManager : MonoBehaviourSingleton<UnitEffectManager>
{

    public void SpawnEffect(Unit target, UnitEffect effect)
    {
        StartCoroutine(Effect(target, effect));
    }

    private IEnumerator Effect(Unit target, UnitEffect effect)
    {
        if (target.UnitAlreadyContainsEffectWithID(effect.effectId)) yield break;

        target.AddEffect(effect);

        yield return new WaitForSeconds(effect.effectDuration);

        target.RemoveEffect(effect);
    }

}
