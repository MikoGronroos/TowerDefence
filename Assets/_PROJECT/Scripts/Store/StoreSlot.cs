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

    private Dictionary<string, object> _currentArgs;

    private Action<Dictionary<string, object>> _onBuyAction;

    public void Initialize(Dictionary<string, object> args, Action<Dictionary<string, object>> callback, Color textColor)
    {

        _currentArgs = args;

        StoreItem item = (StoreItem)args["Item"];

        costText.text = item.Price.ToString();

        costText.color = textColor;

        icon.sprite = item.Icon;

        switch (item.currencyType)
        {
            case CurrencyType.HardCurrency:
                currencyIcon.sprite = hcIcon;
                break;
            case CurrencyType.SoftCurrency:
                currencyIcon.sprite = scIcon;
                break;
        }

        _onBuyAction += callback;

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        //Buy item
        _onBuyAction?.Invoke(_currentArgs);
    }

}
