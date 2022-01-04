using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviourSingleton<EffectManager>
{

    [SerializeField] private List<Effect> currentEffects = new List<Effect>();

    private void Update()
    {
        foreach (var effect in currentEffects)
        {
            effect.Tick();
        }
    }

}
