using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/PlayFabCurrencyEventChannel", fileName ="PlayFabCurrencyEventChannel")]
	public class PlayFabCurrencyEventChannel : EventChannelBase
	{
		public EventChannel ChangeAmountOfSoftCurrency;
		public EventChannel ChangeAmountOfHardCurrency;
	}
}
