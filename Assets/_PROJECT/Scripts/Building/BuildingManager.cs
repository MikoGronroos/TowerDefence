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
                    GameObject turret = PhotonNetwork.Instantiate(buildingPrefab.name, _hit.point, Quaternion.identity);
                }
            }
        }

    }

    private bool CheckIfBuildingIsBlocked(Vector3 checkPos)
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(checkPos, buildingBlockedCheckRadious).Where(temp => temp.GetComponent<Turret>()).ToArray();

        return hits.Length > 0;

    }

    public void SetBuilding(GameObject building)
    {
        buildingPrefab = building;
    }


}
