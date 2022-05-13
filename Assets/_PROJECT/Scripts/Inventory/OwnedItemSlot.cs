using UnityEngine;
using UnityEngine.EventSystems;
using Finark.Utils;

public class OwnedItemSlot : MonoBehaviour, IPointerClickHandler
{

	[SerializeField] private string mainKey;
    [SerializeField] private string graphicKey;

    public void SetData(string main, string graphic)
    {
        mainKey = main;
        graphicKey = graphic;
    }

    public void OnItemEquipped()
    {
        SkinManager.Instance.SetGraphicKeyWithMainKey(mainKey, graphicKey);
    }

    public void OnItemDequipped()
    {
        SkinManager.Instance.SetGraphicKeyWithMainKey(mainKey, "");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryTooltip.Instance.EnableTooltip(MyUtils.GetMouseWorldPosition(), OnItemEquipped, OnItemDequipped);

    }
}
