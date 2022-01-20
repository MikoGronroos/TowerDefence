using UnityEngine;

[CreateAssetMenu(menuName = "DataStructures/Float Addon")]
public class FloatAddon : ScriptableObject
{

    public float Addon;

    public void Add(CustomFloat Value)
    {
        Value.BaseValue += Addon;
    }

}
