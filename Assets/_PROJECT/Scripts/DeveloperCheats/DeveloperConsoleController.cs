using UnityEngine;

public class DeveloperConsoleController : MonoBehaviourSingletonDontDestroyOnLoad<DeveloperConsoleController>
{

    [SerializeField] private Canvas developerConsole;

    [SerializeField] private KeyCode developerConsoleToggleKey;

    private void Update()
    {
        if (Input.GetKeyDown(developerConsoleToggleKey))
        {
            developerConsole.enabled = !developerConsole.enabled;
        }
    }
}
