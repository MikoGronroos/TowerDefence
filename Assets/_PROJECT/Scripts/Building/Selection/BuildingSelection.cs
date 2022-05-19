using UnityEngine;
using Finark.Utils;
using Photon.Pun;
using System.Collections.Generic;
using Finark.Events;

public class BuildingSelection : MonoBehaviourSingleton<BuildingSelection>
{

    [SerializeField] private Building selectedBuilding;

    [SerializeField] private LayerMask turretLayerMask;

    [SerializeField] private TurretEventChannel turretEventChannel;

    private void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector2 touchRay = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            RaycastHit2D hit = Physics2D.Raycast(touchRay, Input.GetTouch(0).position, Mathf.Infinity, turretLayerMask);

            ProcessHit(hit.transform);

        }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(MyUtils.GetMouseWorldPosition(), Vector2.zero, Mathf.Infinity, turretLayerMask);

            ProcessHit(hit.transform);

        }
#endif

    }

    public void SellSelectedBuilding()
    {
        PhotonNetwork.Destroy(selectedBuilding.gameObject.GetPhotonView());

        selectedBuilding = null;

        turretEventChannel?.OnTurretSelected(new Dictionary<string, object> {{ "toggleValue", false },{ "turret", null }});

    }

    private void ProcessHit(Transform hit)
    {

        if (hit == null)
        {
            DeselectBuilding();
            return;
        }

        if (hit.transform.TryGetComponent(out Building building))
        {

            if (building is Turret)
            {
                ProcessTurret(building as Turret);
            }

            if (building is Barrack)
            {
                ProcessBarrack(building as Barrack);
            }

        }
        else
        {
            DeselectBuilding();
        }

    }

    private void ProcessTurret(Turret turret)
    {
        if (turret.TurretOwnerID != PlayerManager.Instance.GetLocalPlayer().GetPlayerID()) return;

        if (turret != selectedBuilding)
        {

            if (selectedBuilding != null)
            {
                RangeVisualisation.Instance.EraseCircle(selectedBuilding.gameObject);
            }

            selectedBuilding = turret;

            turretEventChannel?.OnTurretSelected(new Dictionary<string, object> { { "toggleValue", true }, { "turret", selectedBuilding } });

            RangeVisualisation.Instance.DrawCircle(turret.gameObject, turret.GetTurretExecutable().Range.Value, 0.25f);
        }
    }

    private void ProcessBarrack(Barrack barrack)
    {

    }

    private void DeselectBuilding()
    {
        if (MyUtils.IsPointerOverUI()) return;

        if (selectedBuilding == null) return;

        RangeVisualisation.Instance.EraseCircle(selectedBuilding.gameObject);
        selectedBuilding = null;

        turretEventChannel?.OnTurretSelected(new Dictionary<string, object> { { "toggleValue", false }, { "turret", null } });
    }

}
