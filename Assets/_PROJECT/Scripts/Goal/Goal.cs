using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Finark.Events;

public class Goal : MonoBehaviour
{

    [SerializeField] private int goalOwnerID;

    [SerializeField] private UnitEventChannel unitEventChannel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {

            HealthManager.Instance.RemoveHealthWithID(unit.GetUnitStats().Damage, goalOwnerID);

            unitEventChannel.OnUnitReachedGoal?.Invoke(new Dictionary<string, object> { { "UnitID", unit.GetUnitStats().UnitID }, { "GoalOwnerID", goalOwnerID } });

            PhotonNetwork.Destroy(collision.GetComponent<PhotonView>());

        }
    }

}
