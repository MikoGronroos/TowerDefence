using Finark.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private string currentScene;

    [SerializeField] private string[] scenesToLoad;

    [SerializeField] private SceneManagementEventChannel sceneManagementEventChannel;

    private void Awake()
    {
        foreach (var scene in scenesToLoad)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }

    private void OnEnable()
    {
        sceneManagementEventChannel.UnloadScenes += UnloadScenes;
    }

    private void OnDisable()
    {
        sceneManagementEventChannel.UnloadScenes -= UnloadScenes;
    }

    private void UnloadScenes(Dictionary<string, object> args, System.Action<Dictionary<string, object>> callback)
    {
        foreach (var scene in scenesToLoad)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
        SceneManager.UnloadSceneAsync(currentScene);
    }


}
