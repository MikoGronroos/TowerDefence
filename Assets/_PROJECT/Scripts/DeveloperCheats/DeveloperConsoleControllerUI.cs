using UnityEngine;
using TMPro;

public class DeveloperConsoleControllerUI : MonoBehaviour
{

    [SerializeField] private TMP_InputField commandLine;

    public string GetCommandLineText()
    {
        WhenCommandLineExecuted();
        return commandLine.text;
    }

    private void WhenCommandLineExecuted()
    {
        commandLine.text = "";
    }

}
