using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Finark.Events;

public class ReadyUpUI : MonoBehaviour
{

    [SerializeField] private RoomEventChannel roomEventChannel;

    [SerializeField] private Button readyUpButton;

    private void Awake()
    {

        readyUpButton.onClick.AddListener(() => {

            roomEventChannel.OnPlayerReadyUp?.Invoke(null);

        });
    }

}
