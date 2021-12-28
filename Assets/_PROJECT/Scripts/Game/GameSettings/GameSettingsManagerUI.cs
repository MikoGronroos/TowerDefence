using UnityEngine;
using UnityEngine.UI;

public class GameSettingsManagerUI : MonoBehaviour
{

    [SerializeField] private Toggle singleplayerToggle;

    private void Awake()
    {
        singleplayerToggle.onValueChanged.AddListener(delegate {
            GameSettingsManager.Instance.GetGameSettings().Singleplayer = singleplayerToggle.isOn;
        });
    }

}
