using UnityEngine;

[System.Serializable]
public class UnitEffectWrapper
{
	public UnitEffect Effect;
	public GameObject EffectGameObject;

    public UnitEffectWrapper(UnitEffect effect, GameObject effectGameObject)
    {
        Effect = effect;
        EffectGameObject = effectGameObject;
    }
}