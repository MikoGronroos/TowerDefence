using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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

    [Header("Prefabs")]

    [SerializeField] private GameObject upgradePathPrefab;

    [Header("Parent Transforms")]

    [SerializeField] private Transform upgradePathParent;

    private List<GameObject> drawnUpgradePaths = new List<GameObject>();

    private void Awake()
    {
        sellTurretButton.onClick.AddListener(() => {

            TurretSelection.Instance.SellSelectedTurret();

        });
    }

    public void OpenSelectionUI(Turret turret)
    {

        turretNameText.text = turret.GetTurretStats().Name;

        damageValueText.text = turret.GetTurretStats().Damage.Value.ToString();
        rangeValueText.text = turret.GetTurretStats().Range.Value.ToString();
        attackSpeedValueText.text = turret.GetTurretStats().AttackSpeed.Value.ToString();

        sellPriceText.text = $"{turret.GetTurretStats().SellPrice}$";

        EraseDrawnUpgradePaths();

        foreach (var path in turret.GetUpgradePaths().Paths)
        {
            GameObject pathGameObject = Instantiate(upgradePathPrefab, upgradePathParent);
            drawnUpgradePaths.Add(pathGameObject);
        }

        turretSelectionMenu.SetActive(true);
    }

    public void CloseSelectionUI()
    {
        turretSelectionMenu.SetActive(false);
    }

    private void EraseDrawnUpgradePaths()
    {
        for (int i = drawnUpgradePaths.Count - 1; i >= 0; i--)
        {
            Destroy(drawnUpgradePaths[i]);
        }

        drawnUpgradePaths.Clear();
    }

}
