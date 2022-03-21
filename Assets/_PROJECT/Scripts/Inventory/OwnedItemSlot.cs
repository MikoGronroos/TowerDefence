using UnityEngine;

public class OwnedItemSlot : MonoBehaviour
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

}
