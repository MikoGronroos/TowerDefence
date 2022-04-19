using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BundleSlot : MonoBehaviour
{

    [SerializeField] private Image slotImage;
    [SerializeField] private TextMeshProUGUI slotAmountText;

    public void SetupSlot(Sprite sprite, string amount)
    {
        slotImage.sprite = sprite;
        slotAmountText.text = amount;
    }

}
