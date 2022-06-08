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

            unitEventChannel.OnUnitReachedGoal?.Invoke(new Dictionary<string, object> { { "UnitID", unit.GetUnitStats().UnitID }, { "GoalOwnerID", goalOwnerID } });

            if (collision.GetComponent<PhotonView>().IsMine)
            {

                HealthManager.Instance.RemoveHealthWithID(unit.GetUnitStats().Damage, goalOwnerID);

                UnitSpawner.Instance.DespawnUnit(collision.gameObject);

            }

        }
    }

}
