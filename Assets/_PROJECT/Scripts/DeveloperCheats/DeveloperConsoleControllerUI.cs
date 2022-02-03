using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DeveloperConsoleControllerUI : MonoBehaviour
{

    [SerializeField] private TMP_InputField commandLine;

    [SerializeField] private GameObject commandPrintPrefab;

    [SerializeField] private Transform commandPrintParent;

    [SerializeField] private Color errorColor;
    [SerializeField] private Color successColor;

    [SerializeField] private List<GameObject> commandPrints = new List<GameObject>();

    public string GetCommandLineText()
    {
        string text = commandLine.text;
        WhenCommandLineExecuted();
        return text;
    }

    public void SetCommandLineText(string text)
    {
        commandLine.text = text;
    }

    public void Print(string text, PrintType type)
    {
        GameObject textBox = Instantiate(commandPrintPrefab, commandPrintParent);
        var textElement = textBox.GetComponent<TextMeshProUGUI>();
        textElement.text = text;
        commandPrints.Add(textBox);

        switch (type)
        {
            case PrintType.Error:
                textElement.color = errorColor;
                break;
            case PrintType.Success:
                textElement.color = successColor;
                break;
        }

    }

    public void ClearConsolePrints()
    {

        foreach (var print in commandPrints)
        {
            Destroy(print);
        }

        commandPrints.Clear();

    }

    private void WhenCommandLineExecuted()
    {
        commandLine.text = "";
    }

}


public enum PrintType
{
    Success,
    Error
}