using UnityEngine;
using UnityEngine.SceneManagement;

public class DeveloperHandler : MonoBehaviour
{

    public void CheckIfDeveloper()
    {
#if UNITY_EDITOR

        Debug.Log("Loading Developer Console!");

        SceneManager.LoadScene("DeveloperConsole", LoadSceneMode.Additive);

#endif
    }

}
