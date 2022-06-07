using Finark.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{

	[SerializeField] private Button toggleSettingsMenuButton;

    [SerializeField] private GameObject settingsMenuPanel;
	[SerializeField] private Button surrenderButton;

    [SerializeField] private RoomEventChannel roomEventChannel;

    private void Awake()
    {
        toggleSettingsMenuButton.onClick.AddListener(()=> {
            settingsMenuPanel.SetActive(!settingsMenuPanel.activeSelf);
        });

        surrenderButton.onClick.AddListener(()=> {
            roomEventChannel.OnPlayerSurrender?.Invoke(new Dictionary<string, object> { { "loserID", PlayerManager.Instance.GetLocalPlayer().GetPlayerID() } });
        });

    }

}
