using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Finark.Events;

public class NameChangerUI : MonoBehaviour
{

    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button nameApplyButton;

    [SerializeField] private AccountEventChannel accountEventChannel;

    private void Awake()
    {
        nameApplyButton.onClick.AddListener(ChangeName);
    }

    private void ChangeName()
    {
        var newName = nameInputField.text;
        nameInputField.text = "";
        accountEventChannel.OnNameChanged?.Invoke(new Dictionary<string, object> { { "Name", newName } });
    }

}
