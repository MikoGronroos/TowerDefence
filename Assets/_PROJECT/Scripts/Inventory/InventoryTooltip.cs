using Finark.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviourSingleton<InventoryTooltip>
{

    [SerializeField] private GameObject tooltipObject;

    [SerializeField] private Button equipSkinButton;
    [SerializeField] private Button dequipSkinButton;

    private void Start()
    {
        DisableTooltip();
    }

    public void EnableTooltip(Vector3 position, UnityAction equipCallback, UnityAction dequipCallback)
    {

        RemoveListenersFromButtons();

        equipSkinButton.onClick.AddListener(()=> {
            equipCallback();
            DisableTooltip();
        });

        dequipSkinButton.onClick.AddListener(() => {
            dequipCallback();
            DisableTooltip();
        });

        tooltipObject.transform.position = position;
        tooltipObject.SetActive(true);
    }

    public void DisableTooltip()
    {
        RemoveListenersFromButtons();
        tooltipObject.SetActive(false);
    }

    private void RemoveListenersFromButtons()
    {
        dequipSkinButton.onClick.RemoveAllListeners();
        equipSkinButton.onClick.RemoveAllListeners();
    }

}
