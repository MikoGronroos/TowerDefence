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

        damageValueText.text = turret.GetTurretExecutable().Damage.Value.ToString();
        rangeValueText.text = turret.GetTurretExecutable().Range.Value.ToString();
        attackSpeedValueText.text = turret.GetTurretExecutable().AttackSpeed.Value.ToString();

        sellPriceText.text = $"{turret.GetTurretStats().SellPrice}$";

        EraseDrawnUpgradePaths();

        int index = 0;

        foreach (var path in turret.GetUpgradePaths().Paths)
        {


            GameObject pathGameObject = Instantiate(upgradePathPrefab, upgradePathParent);
            var holder = pathGameObject.GetComponent<TurretUpgradeHolder>();

            holder.ChangeIconSprite(turret.GetUpgradePaths().Paths[index].Upgrades[turret.GetTurretPathIndex()[index]].Icon);

            holder.UpgradePathIndex = index;
            holder.OwnerTurret = turret;

            drawnUpgradePaths.Add(pathGameObject);

            index++;

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
