using UnityEngine;

public class PlayerManager : MonoBehaviourSingletonDontDestroyOnLoad<PlayerManager>
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
}
