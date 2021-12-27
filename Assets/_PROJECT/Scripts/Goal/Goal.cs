using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class Goal : MonoBehaviour
{

    [SerializeField] private int goalOwnerID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {

            HealthManager.Instance.RemoveHealthWithID(unit.GetUnitStats().Damage, goalOwnerID);

            EventManager.InvokeEvent("OnUnitReachedGoal", new Dictionary<string, object> { { "UnitID", unit.GetUnitStats().UnitID }, { "GoalOwnerID", goalOwnerID } });

            PhotonNetwork.Destroy(collision.GetComponent<PhotonView>());

        }
    }

}
