using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviourSingleton<EffectManager>
{

    [SerializeField] private List<Effect> currentEffects = new List<Effect>();

    public void AddEffect(Effect effect)
    {
        if (!currentEffects.Contains(effect))
        {
            currentEffects.Add(effect);
        }
    }

    public void RemoveEffect(Effect effect)
    {
        if (currentEffects.Contains(effect))
        {
            currentEffects.Remove(effect);
        }
    }
}
