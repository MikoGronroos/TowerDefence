using UnityEngine;
using Finark.Utils;
using Photon.Pun;
using System.Linq;

public class BuildingManager : MonoBehaviourSingleton<BuildingManager>
{

    [Header("Placement Options")]
    [SerializeField] private float buildingBlockedCheckRadious;

    private RaycastHit2D _hit;

    private ShopItemBuilding _currentlyBuilding;

    private int _price;

    private bool _isBuilding;

    private void Update()
    {

        if (_currentlyBuilding == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            
            if (MyUtils.IsPointerOverUI()) return;

            _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

            if (CheckIfBuildingIsBlocked(_hit.point)) return;

            if (_hit.transform.TryGetComponent(out Board board))
            {

                if (_currentlyBuilding.PlaceOnLocalBoard)
                {
                    if (board.GetID() == PlayerManager.Instance.GetLocalPlayer().GetPlayerID())
                    {
                        var building = BuildingSpawner.Instance.SpawnBuilding(_currentlyBuilding.ItemPrefab.name, _hit.point);
                    }
                }
                else
                {
                    var building = BuildingSpawner.Instance.SpawnBuilding(_currentlyBuilding.ItemPrefab.name, _hit.point);
                }

                VirtualCurrencyManager.Instance.RemoveCurrency(_price);
                _currentlyBuilding = null;
                _price = 0;
                _isBuilding = false;

            }
        }

    }

    private bool CheckIfBuildingIsBlocked(Vector3 checkPos)
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(checkPos, buildingBlockedCheckRadious).Where(temp => temp.GetComponent<Turret>()).ToArray();

        return hits.Length > 0;

    }

    public void SetBuilding(ShopItemBuilding building)
    {
        _currentlyBuilding = building;
        _price = building.Cost;
        _isBuilding = true;
    }

    public bool IsBuilding()
    {
        return _isBuilding;
    }

}
