using UnityEngine;

public class PlayerManager : MonoBehaviourSingletonDontDestroyOnLoad<PlayerManager>
{

    [SerializeField] private LocalPlayer localPlayer;

    public void AddLocalPlayer(LocalPlayer player)
    {
        Debug.Log(transform.name);
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
