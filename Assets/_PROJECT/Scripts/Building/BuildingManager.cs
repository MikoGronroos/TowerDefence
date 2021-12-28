using UnityEngine;
using Finark.Utils;
using Photon.Pun;
using System.Linq;

public class BuildingManager : MonoBehaviourSingleton<BuildingManager>
{

    [SerializeField] private GameObject buildingPrefab;

    [Header("Placement Options")]
    [SerializeField] private float buildingBlockedCheckRadious;

    private RaycastHit2D _hit;

    private int _price;

    private void Update()
    {

        if (buildingPrefab == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            
            if (MyUtils.IsPointerOverUI()) return;

            _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

            if (CheckIfBuildingIsBlocked(_hit.point)) return;

            if (_hit.transform.TryGetComponent(out Board board))
            {
                if (board.GetID() == PlayerManager.Instance.GetLocalPlayer().GetPlayerID())
                {
                    TurretSpawner.Instance.SpawnTurret(buildingPrefab.name, _hit.point);
                    VirtualCurrencyManager.Instance.RemoveCurrency(_price);
                    buildingPrefab = null;
                    _price = 0;
                }
            }
        }

    }

    private bool CheckIfBuildingIsBlocked(Vector3 checkPos)
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(checkPos, buildingBlockedCheckRadious).Where(temp => temp.GetComponent<Turret>()).ToArray();

        return hits.Length > 0;

    }

    public void SetBuilding(GameObject building, int price)
    {
        buildingPrefab = building;
        _price = price;
    }


}
