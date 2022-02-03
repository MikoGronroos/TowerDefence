using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DeveloperConsoleControllerUI : MonoBehaviour
{

    [SerializeField] private TMP_InputField commandLine;

    [SerializeField] private GameObject commandPrintPrefab;

    [SerializeField] private Transform commandPrintParent;

    [SerializeField] private List<GameObject> commandPrints = new List<GameObject>();

    public string GetCommandLineText()
    {
        string text = commandLine.text;
        WhenCommandLineExecuted();
        return text;
    }

    public void Print(string text)
    {
        GameObject textBox = Instantiate(commandPrintPrefab, commandPrintParent);
        textBox.GetComponent<TextMeshProUGUI>().text = text;
        commandPrints.Add(textBox);
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
