using UnityEngine;
using Photon.Pun;

public class Decal : MonoBehaviour
{

    [SerializeField] private float timeUntilDestroy;

    private void Start()
    {
        Invoke("Delete", timeUntilDestroy);
    }

    private void Delete()
    {
        PhotonNetwork.Destroy(gameObject.GetPhotonView());
    }

}
