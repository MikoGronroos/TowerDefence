using UnityEngine;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{

    [SerializeField] private LocalPlayer localPlayer;

    public void AddLocalPlayer(LocalPlayer player)
    {
        localPlayer = player;
    }

    public LocalPlayer GetLocalPlayer()
    {
        return localPlayer;
    }

    public bool HasLocalPlayer()
    {

        return localPlayer != null;

    }

}
