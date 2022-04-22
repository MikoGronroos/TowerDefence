using UnityEngine;
using Finark.Utils;
using Photon.Pun;
using System.Linq;

public class BuildingManager : MonoBehaviourSingleton<BuildingManager>
{

    [Header("Placement Options")]
    [SerializeField] private float buildingBlockedCheckRadious;

    private RaycastHit2D _hit;

    private bool CheckIfBuildingIsBlocked(Vector3 checkPos)
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(checkPos, buildingBlockedCheckRadious).Where(temp => temp.GetComponent<Turret>()).ToArray();

        return hits.Length > 0;

    }

    public bool Build(Vector3 buildSpot, ShopItemBuilding build)
    {
        if (MyUtils.IsPointerOverUI()) return false;

        _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

        if (CheckIfBuildingIsBlocked(buildSpot)) return false;

        if (_hit.transform == null) return false;

        if (_hit.transform.TryGetComponent(out Board board))
        {

            if (build.PlaceOnLocalBoard)
            {
                if (board.GetID() == PlayerManager.Instance.GetLocalPlayer().GetPlayerID())
                {
                    var building = BuildingSpawner.Instance.SpawnBuilding(build.ItemPrefab.name, buildSpot);
                    return true;
                }
            }
            else
            {
                var building = BuildingSpawner.Instance.SpawnBuilding(build.ItemPrefab.name, buildSpot);
                return true;
            }
        }
        return false;
    }
}
