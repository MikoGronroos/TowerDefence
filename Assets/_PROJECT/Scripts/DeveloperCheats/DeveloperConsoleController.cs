using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeveloperConsoleController : MonoBehaviourSingletonDontDestroyOnLoad<DeveloperConsoleController>
{

    [SerializeField] private Canvas developerConsole;

    [SerializeField] private KeyCode developerConsoleToggleKey;

    [SerializeField] private List<BaseCommand> consoleCommands = new List<BaseCommand>();

    private List<string> _previousCommands = new List<string>();
    private int _previousCommandIndex = 0;

    private DeveloperConsoleControllerUI _developerConsoleControllerUI;

    private void Awake()
    {
        _developerConsoleControllerUI = GetComponent<DeveloperConsoleControllerUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(developerConsoleToggleKey))
        {
            developerConsole.enabled = !developerConsole.enabled;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ProcessCommand(_developerConsoleControllerUI.GetCommandLineText());
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            _previousCommandIndex++;

            if (_previousCommandIndex > _previousCommands.Count)
            {
                _previousCommandIndex = 1;
            }

            _developerConsoleControllerUI.SetCommandLineText(_previousCommands[_previousCommandIndex - 1]); // If _previousCommandIndex == 1 then this is index 0

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (_previousCommandIndex <= 1)
            {
                _previousCommandIndex = _previousCommands.Count;
            }
            else
            {
                _previousCommandIndex--;
            }

            _developerConsoleControllerUI.SetCommandLineText(_previousCommands[_previousCommandIndex - 1]);

        }
    }

    public void ProcessCommand(string inputValue)
    {

        _previousCommands.Add(inputValue);

        string[] inputSplit = inputValue.Split(' ');

        string commandInput = inputSplit[0];

        string[] args = inputSplit.Skip(1).ToArray();

        bool executed = ExecuteCommand(commandInput, args);

        if (!executed)
        {
            PrintToConsole($"The keyword: {commandInput} Was Not Found. Please Enter A Valid Command.", PrintType.Error);
        }

    }

    public void PrintToConsole(string text, PrintType type) => _developerConsoleControllerUI.Print(text, type);

    public void ClearConsole() => _developerConsoleControllerUI.ClearConsolePrints();

    private bool ExecuteCommand(string commandInput, string[] args)
    {
        foreach (var command in consoleCommands)
        {
            if (!commandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (command.Process(args))
            {
                return true;
            }

        }
        return false;
    }

}
