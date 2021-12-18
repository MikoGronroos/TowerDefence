using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyUpUI : MonoBehaviour
{

    [SerializeField] private Button readyUpButton;

    private ReadyUp _readyUp;

    private void Awake()
    {

        _readyUp = GetComponent<ReadyUp>();

        readyUpButton.onClick.AddListener(() => {

            _readyUp.LocalPlayerReadyUp();
        
        });
    }

}
