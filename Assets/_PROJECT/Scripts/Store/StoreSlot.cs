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

    private StoreItem _currentItem;

    private Action<Dictionary<string, object>> _onBuyAction;

    public void Initialize(StoreItem item, Action<Dictionary<string, object>> callback, Color textColor)
    {

        _currentItem = item;

        //currencyIcon.sprite = item.currencyType ? hcIcon : scIcon;

        costText.text = item.Price.ToString();

        costText.color = textColor;

        icon.sprite = item.Icon;

        _onBuyAction += callback;

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        //Buy item
        _onBuyAction?.Invoke(new Dictionary<string, object> { { "Item", _currentItem } });
    }

}
