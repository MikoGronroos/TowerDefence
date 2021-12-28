using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviourSingleton<GameSettingsManager>
{

    [SerializeField] private GameSettings currentSettings;
    


    public GameSettings GetGameSettings()
    {
        return currentSettings;
    }

}
