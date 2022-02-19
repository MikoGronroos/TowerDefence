using Finark.Events;
using UnityEngine;

public class GameEnd : MonoBehaviour
{

    [SerializeField] private SceneManagementEventChannel sceneManagementEventChannel;

    private void Start()
    {
        sceneManagementEventChannel.GameEndSceneLoaded(null);
    }

}
