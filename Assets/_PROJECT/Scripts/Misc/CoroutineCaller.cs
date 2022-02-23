using System.Collections;

public class CoroutineCaller : MonoBehaviourSingletonDontDestroyOnLoad<CoroutineCaller>
{
	public void StartChildCoroutine(IEnumerator method)
    {
        StartCoroutine(method);
    }

}
