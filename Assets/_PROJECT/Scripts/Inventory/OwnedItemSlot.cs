using UnityEngine;
using UnityEngine.EventSystems;

public class OwnedItemSlot : MonoBehaviour, IPointerClickHandler
{

	[SerializeField] private string mainKey;
    [SerializeField] private string graphicKey;

    public void SetData(string main, string graphic)
    {
        mainKey = main;
        graphicKey = graphic;
    }

    public void OnItemClicked()
    {
        SkinManager.Instance.SetGraphicKeyWithMainKey(mainKey, graphicKey);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemClicked();
    }
}
