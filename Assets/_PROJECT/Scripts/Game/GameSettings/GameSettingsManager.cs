using UnityEngine;

public class GameSettingsManager : MonoBehaviourSingletonDontDestroyOnLoad<GameSettingsManager>
{

    [SerializeField] private GameSettings currentSettings;

    public GameSettings GetGameSettings()
    {
        return currentSettings;
    }

}
