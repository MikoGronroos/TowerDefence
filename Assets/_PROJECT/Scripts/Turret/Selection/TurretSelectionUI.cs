using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretSelectionUI : MonoBehaviour
{

    [SerializeField] private GameObject turretSelectionMenu;

    [Header("Selection Menu References")]

    [SerializeField] private TextMeshProUGUI turretNameText;

    [SerializeField] private Button sellTurretButton;
    [SerializeField] private TextMeshProUGUI sellPriceText;

    public void OpenSelectionUI(Turret turret)
    {

        turretNameText.text = turret.GetTurretStats().Name;

        turretSelectionMenu.SetActive(true);
    }

    public void CloseSelectionUI()
    {
        turretSelectionMenu.SetActive(false);
    }

}
