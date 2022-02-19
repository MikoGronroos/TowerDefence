using UnityEngine;
using Finark.Utils;
using Photon.Pun;
using System.Collections.Generic;
using Finark.Events;

public class TurretSelection : MonoBehaviourSingleton<TurretSelection>
{

    [SerializeField] private Turret selectedTurret;

    [SerializeField] private TurretEventChannel turretEventChannel;

    private RangeVisualisation _rangeVisualisation;

    private void Awake()
    {
        _rangeVisualisation = GetComponent<RangeVisualisation>();
    }

    private void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector2 touchRay = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            RaycastHit2D hit = Physics2D.Raycast(touchRay, (Input.GetTouch(0).position));

            ProcessHit(hit.transform);

        }

#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(MyUtils.GetMouseWorldPosition(), Vector2.zero);

            ProcessHit(hit.transform);

        }

#endif

    }

    public void SellSelectedTurret()
    {
        PhotonNetwork.Destroy(selectedTurret.gameObject.GetPhotonView());

        VirtualCurrencyManager.Instance.AddCurrency(selectedTurret.GetTurretStats().SellPrice);

        selectedTurret = null;

        turretEventChannel?.OnTurretSelected(new Dictionary<string, object> {{ "toggleValue", false },{ "turret", null }});

    }

    private void ProcessHit(Transform hit)
    {

        if (hit == null)  return;

        if (hit.transform.TryGetComponent(out Turret turret))
        {

            if (turret.TurretOwnerID != PlayerManager.Instance.GetLocalPlayer().GetPlayerID()) return;

            if (turret != selectedTurret)
            {

                if (selectedTurret != null)
                {
                    _rangeVisualisation.EraseCircle(selectedTurret.gameObject);
                }

                selectedTurret = turret;

                turretEventChannel?.OnTurretSelected(new Dictionary<string, object> {{ "toggleValue", true },{ "turret", selectedTurret }});

                _rangeVisualisation.DrawCircle(turret.gameObject, turret.GetTurretExecutable().Range.Value, .3f);
            }
        }
        else
        {
            if (MyUtils.IsPointerOverUI()) return;

            if (selectedTurret == null) return;

            _rangeVisualisation.EraseCircle(selectedTurret.gameObject);
            selectedTurret = null;

            turretEventChannel?.OnTurretSelected(new Dictionary<string, object> {{ "toggleValue", false },{ "turret", null }});

        }

    }

}
