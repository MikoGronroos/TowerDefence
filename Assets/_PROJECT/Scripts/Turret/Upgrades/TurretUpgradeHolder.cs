using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretUpgradeHolder : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Image icon;

    public int UpgradePathIndex { get; set; }

    public Turret OwnerTurret { private get; set; }

    public void ChangeIconSprite(Sprite icon)
    {
        this.icon.sprite = icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OwnerTurret.UpgaredTurret(UpgradePathIndex);
    }
}
