using Finark.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] private string currentScene;

    [SerializeField] private string[] scenesToLoad;

    [SerializeField] private string[] scenesToUnload;

    [SerializeField] private SceneManagementEventChannel sceneManagementEventChannel;

    [SerializeField] private bool loadScenes;
    [SerializeField] private bool unloadScenes;

    [SerializeField] private bool loadScenesOnAwake;
    [SerializeField] private bool loadScenesOnStart;


    private void Awake()
    {
        if (loadScenesOnAwake)
        {
            LoadScenes();
        }
    }

    private void Start()
    {
        if (loadScenesOnStart)
        {
            LoadScenes();
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

    private void LoadScenes()
    {
        if (!loadScenes) return;

        foreach (var scene in scenesToLoad)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }

        sceneManagementEventChannel.OnScenesLoaded?.Invoke(null);

    }

    private void UnloadScenes(Dictionary<string, object> args, System.Action<Dictionary<string, object>> callback)
    {

        if (!unloadScenes) return;

        foreach (var scene in scenesToUnload)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
        SceneManager.UnloadSceneAsync(currentScene);

        callback?.Invoke(null);

    }


}
