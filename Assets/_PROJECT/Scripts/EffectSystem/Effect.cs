using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject
{

    public Sprite Icon;

    public List<object> EffectTargets = new List<object>();

    public abstract void Tick();

}