using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviourSingleton<EffectManager>
{

    [SerializeField] private List<Effect> currentEffects = new List<Effect>();

    [SerializeField] private float effectTickInterval;

    private void Start()
    {
        StartCoroutine(EffectTick());
    }

    private IEnumerator EffectTick()
    {

        foreach (var effect in currentEffects)
        {
            effect.Tick();
        }

        yield return new WaitForSeconds(effectTickInterval);
    }

}
