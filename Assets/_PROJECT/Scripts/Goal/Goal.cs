using UnityEngine;
using Photon.Pun;

public class Goal : MonoBehaviour
{

    [SerializeField] private int goalOwnerID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {

            HealthManager.Instance.RemoveHealthWithID(unit.GetUnitStats().Damage, goalOwnerID);

            PhotonNetwork.Destroy(collision.GetComponent<PhotonView>());
        }
    }

}
