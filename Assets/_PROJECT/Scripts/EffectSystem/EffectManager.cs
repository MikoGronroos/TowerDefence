using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviourSingleton<EffectManager>
{

    public Dictionary<int, Effect> EffectDictionary = new Dictionary<int, Effect>();

    public Sprite GetEffectIcon(string iconName)
    {
        return Resources.Load("Icons/Effects/" + iconName) as Sprite;
    }

}
