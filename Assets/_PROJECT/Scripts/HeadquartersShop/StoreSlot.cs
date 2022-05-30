using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using System.Collections.Generic;

public class StoreSlot : MonoBehaviour, IPointerClickHandler
{

	[SerializeField] private Image icon;

	[SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private Image currencyIcon;

	[SerializeField] private Sprite hcIcon;
	[SerializeField] private Sprite scIcon;

    [Header("Slot Text")]
    [SerializeField] private Color allowedToBuyColor;
    [SerializeField] private Color notAllowedToBuyColor;

    private StoreItem _currentStoreItem;

    private Action<Dictionary<string, object>> _onBuyAction;

    public void Initialize(StoreItem storeItem, Action<Dictionary<string, object>> callback)
    {

        _currentStoreItem = storeItem;

        costText.text = _currentStoreItem.Price.ToString();

        icon.sprite = _currentStoreItem.Icon;

        switch (_currentStoreItem.currencyType)
        {
            case CurrencyType.HardCurrency:
                currencyIcon.sprite = hcIcon;
                costText.color = PlayFabCurrencyManager.Instance.HasEnoughtHardCurrency((int)_currentStoreItem.Price) ? allowedToBuyColor : notAllowedToBuyColor;
                break;
            case CurrencyType.SoftCurrency:
                currencyIcon.sprite = scIcon;
                costText.color = PlayFabCurrencyManager.Instance.HasEnoughtSoftCurrency((int)_currentStoreItem.Price) ? allowedToBuyColor : notAllowedToBuyColor;
                break;
        }

        _onBuyAction += callback;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Buy item
        _onBuyAction?.Invoke(new Dictionary<string, object> { { "Item", _currentStoreItem } });
    }

}
