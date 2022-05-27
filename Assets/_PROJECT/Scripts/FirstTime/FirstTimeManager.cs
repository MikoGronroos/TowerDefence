using Finark.Events;
using UnityEngine;

public class FirstTimeManager : MonoBehaviour
{
	[SerializeField] private HeadquartersEventChannel headquartersEventChannel;

    private void Start()
    {
        if(AccountManager.Instance.CurrentAccount.IsFirstLaunchOfTheGame) headquartersEventChannel.OnFirstTimeGameLoaded?.Invoke();
    }

}
