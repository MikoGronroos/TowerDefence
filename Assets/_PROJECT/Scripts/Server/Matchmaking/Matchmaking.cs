using Photon.Pun;

public class Matchmaking : MonoBehaviourPunCallbacks
{

    public void EnterQueue()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void ExitQueue()
    {

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
