using System.Collections;

public class CoroutineCaller : MonoBehaviourSingletonDontDestroyOnLoad<CoroutineCaller>
{
	public void StartChildCoroutine(IEnumerator method)
    {
        StartCoroutine(method);
    }

    public void StopChildCoroutine(IEnumerator method)
    {
        StopCoroutine(method);
    }

}
