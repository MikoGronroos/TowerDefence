using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item/Building")]
public class ShopItemBuilding : ShopItem
{

    public bool PlaceOnLocalBoard;
    
    public override void Buy()
    {
        BuyAction();
    }

    public override void BuyAction()
    {
    }


}

