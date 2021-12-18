using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks
{

    public static void LeaveTheRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Headquarter");
    }

}
