using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using Finark.Events;

public class TurretSelectionUI : MonoBehaviour
{

    [Header("Selection Menu References")]

    [SerializeField] private GameObject turretSelectionMenu;

    [SerializeField] private TextMeshProUGUI turretNameText;

    [SerializeField] private TextMeshProUGUI damageValueText;
    [SerializeField] private TextMeshProUGUI rangeValueText;
    [SerializeField] private TextMeshProUGUI attackSpeedValueText;

    [SerializeField] private Button sellTurretButton;
    [SerializeField] private TextMeshProUGUI sellPriceText;

    [Header("Prefabs")]

    [SerializeField] private GameObject upgradePathPrefab;

    [Header("Sprites")]
    [SerializeField] private Sprite upgradePathFullyUpgradedIcon;

    [Header("Parent Transforms")]

    [SerializeField] private Transform upgradePathParent;

    [Header("Event Channels")]

    [SerializeField] private TurretEventChannel turretEventChannel;

    private List<GameObject> drawnUpgradePaths = new List<GameObject>();

    private void Awake()
    {
        sellTurretButton.onClick.AddListener(() => {

            TurretSelection.Instance.SellSelectedTurret();

        });
    }

    private void OnEnable()
    {
        turretEventChannel.OnTurretSelected += ToggleTurretSelection;
    }

    private void OnDisable()
    {
        turretEventChannel.OnTurretSelected -= ToggleTurretSelection;
    }

    private void ToggleTurretSelection(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {

        bool toggleValue = (bool)args["toggleValue"];
        Turret turret = (Turret)args["turret"];

        if (toggleValue)
        {
            OpenSelectionUI(turret);
        }
        else
        {
            CloseSelectionUI();
        }
    }

    private void OpenSelectionUI(Turret turret)
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

            drawnUpgradePaths.Add(pathGameObject);

            if (turret.UpgradePathFullyUpgraded(index))
            {
                holder.ChangeIconSprite(upgradePathFullyUpgradedIcon);
            }
            else
            {
                holder.ChangeIconSprite(turret.GetUpgradePaths().Paths[index].Upgrades[turret.GetTurretPathIndex()[index]].Icon);
            }

            holder.UpgradePathIndex = index;
            holder.OwnerTurret = turret;

            index++;

        }
        turretSelectionMenu.SetActive(true);
    }

    private void CloseSelectionUI()
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
