using UnityEngine;
using TMPro;

public class InGameShopTooltipManager : MonoBehaviourSingleton<InGameShopTooltipManager>
{

	[SerializeField] private GameObject tooltipObject;

    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    [SerializeField] private float durationThreshold = 1.0f;

    [SerializeField] private Vector3 tooltipOffset;

    private bool _isPointerDown = false;
    private bool _longPressTriggered = false;
    private float _timePressStarted;

    #region UI

    [Header("UserInterface")]

    [SerializeField] private TextMeshProUGUI damageValueText;
    [SerializeField] private TextMeshProUGUI attackSpeedValueText;
    [SerializeField] private TextMeshProUGUI penetrationValueText;

    [SerializeField] private GameObject damagePanel;
    [SerializeField] private GameObject attackSpeedPanel;
    [SerializeField] private GameObject penetrationPanel;

    #endregion

    private Vector3 _pos;
    private Turret _turret;
    private Unit _unit;

    private void Update()
    {
        if (_isPointerDown && !_longPressTriggered)
        {
            if (Time.time - _timePressStarted > durationThreshold)
            {
                _longPressTriggered = true;
                EnableTooltip(_pos, _turret, _unit);
            }
        }
    }

    public void StartEnablingTooltip(Vector3 pos, Turret turret = null, Unit unit = null)
    {
        _timePressStarted = Time.time;
        _isPointerDown = true;
        _pos = pos;
        _turret = turret;
        _unit = unit;
    }

    private void EnableTooltip(Vector3 pos, Turret turret = null, Unit unit = null)
    {

        damagePanel.SetActive(true);
        attackSpeedPanel.SetActive(false);
        penetrationPanel.SetActive(false);

        if (turret != null)
        {

            attackSpeedPanel.SetActive(true);
            penetrationPanel.SetActive(true);

            damageValueText.text = turret.GetTurretExecutable().Damage.BaseValue.ToString();
            attackSpeedValueText.text = turret.GetTurretExecutable().AttackSpeed.BaseValue.ToString();
            penetrationValueText.text = turret.GetTurretExecutable().ProjectilePenetration.ToString();
        }

        if (unit != null)
        {
            damageValueText.text = unit.GetUnitStats().Damage.ToString();
        }

        tooltipObject.transform.position = pos + tooltipOffset;
        tooltipObject.SetActive(true);
    }

    public void DisableTooltip()
    {
        tooltipObject.SetActive(false);
        _isPointerDown = false;
        _longPressTriggered = false;
    }

}
