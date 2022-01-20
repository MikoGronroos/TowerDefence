using UnityEngine;

public abstract class TurretUpgrade : ScriptableObject
{

    public string Name;

    public Sprite Icon;

    public abstract void UseUpgrade();

}
