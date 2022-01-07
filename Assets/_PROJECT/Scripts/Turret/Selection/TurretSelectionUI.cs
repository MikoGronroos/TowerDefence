using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretSelectionUI : MonoBehaviour
{

    [SerializeField] private GameObject turretSelectionMenu;

    [Header("Selection Menu References")]

    [SerializeField] private TextMeshProUGUI turretNameText;

    [SerializeField] private TextMeshProUGUI damageValueText;
    [SerializeField] private TextMeshProUGUI rangeValueText;
    [SerializeField] private TextMeshProUGUI attackSpeedValueText;

    [SerializeField] private Button sellTurretButton;
    [SerializeField] private TextMeshProUGUI sellPriceText;

    public void OpenSelectionUI(Turret turret)
    {

        turretNameText.text = turret.GetTurretStats().Name;

        damageValueText.text = turret.GetTurretStats().Damage.Value.ToString();
        rangeValueText.text = turret.GetTurretStats().Range.Value.ToString();
        attackSpeedValueText.text = turret.GetTurretStats().AttackSpeed.Value.ToString();

        turretSelectionMenu.SetActive(true);
    }

    public void CloseSelectionUI()
    {
        turretSelectionMenu.SetActive(false);
    }

}
