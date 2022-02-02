using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeveloperConsoleController : MonoBehaviourSingletonDontDestroyOnLoad<DeveloperConsoleController>
{

    [SerializeField] private Canvas developerConsole;

    [SerializeField] private KeyCode developerConsoleToggleKey;

    [SerializeField] private List<BaseCommand> consoleCommands = new List<BaseCommand>();

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

    }

    public void ProcessCommand(string inputValue)
    {
        string[] inputSplit = inputValue.Split(' ');

        string commandInput = inputSplit[0];

        string[] args = inputSplit.Skip(1).ToArray();

        ExecuteCommand(commandInput, args);

    }

    private void ExecuteCommand(string commandInput, string[] args)
    {
        foreach (var command in consoleCommands)
        {
            if (!commandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (command.Process(args))
            {
                return;
            }

        }
    }
}
