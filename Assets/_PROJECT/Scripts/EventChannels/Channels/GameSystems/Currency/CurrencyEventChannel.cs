using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/CurrencyEventChannel", fileName ="CurrencyEventChannel")]
	public class CurrencyEventChannel : EventChannelBase
	{

		public EventChannel OnCurrencyChanged;
		public EventChannel OnCurrencyIncomeChanged;
		public EventChannel OnCurrencyIntervalUpdate;

		public EventChannel OnMoneyUsed;

	}
}
