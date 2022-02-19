using Finark.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private string currentScene;

    [SerializeField] private string[] scenesToLoad;

    [SerializeField] private SceneManagementEventChannel sceneManagementEventChannel;

    [SerializeField] private bool unloadScenes;

    private void Awake()
    {
        foreach (var scene in scenesToLoad)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }

    private void OnEnable()
    {

        if (!unloadScenes) return;

        sceneManagementEventChannel.UnloadScenes += UnloadScenes;
    }

    private void OnDisable()
    {
        if (!unloadScenes) return;

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
