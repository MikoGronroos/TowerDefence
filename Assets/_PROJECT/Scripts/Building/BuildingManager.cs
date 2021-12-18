using UnityEngine;
using Finark.Utils;
using Photon.Pun;

public class BuildingManager : MonoBehaviourSingleton<BuildingManager>
{

    [SerializeField] private GameObject buildingPrefab;

    private RaycastHit2D _hit;

    private void Update()
    {

        if (buildingPrefab == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            
            if (MyUtils.IsPointerOverUI()) return;

            _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

            if (_hit.transform.TryGetComponent(out Board board))
            {
                if (board.GetID() == PlayerManager.Instance.GetLocalPlayer().GetPlayerID())
                {
                    GameObject turret = PhotonNetwork.Instantiate(buildingPrefab.name, _hit.point, Quaternion.identity);
                }
            }
        }

    }

    private void CheckIfBuildingIsBlocked()
    {

    }

    public void SetBuilding(GameObject building)
    {
        buildingPrefab = building;
    }


}
