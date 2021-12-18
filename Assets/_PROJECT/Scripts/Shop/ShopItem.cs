using UnityEngine;

public abstract class ShopItem : ScriptableObject
{

    public int Cost;
    public Sprite Icon;

    public GameObject itemPrefab;

    public abstract void Buy();

}
