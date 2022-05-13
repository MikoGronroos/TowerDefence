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
        equipSkinButton.onClick.AddListener(()=> {
            equipCallback();
            equipSkinButton.onClick.RemoveListener(equipCallback);
            DisableTooltip();
        });

        dequipSkinButton.onClick.AddListener(() => {
            dequipCallback();
            dequipSkinButton.onClick.RemoveListener(dequipCallback);
            DisableTooltip();
        });

        tooltipObject.transform.position = position;
        tooltipObject.SetActive(true);
    }

    public void DisableTooltip()
    {
        tooltipObject.SetActive(false);
    }

}
