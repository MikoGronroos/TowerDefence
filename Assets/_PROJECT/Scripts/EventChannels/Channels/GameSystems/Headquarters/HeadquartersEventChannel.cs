using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/HeadquartersEventChannel", fileName = "HeadquartersEventChannel")]
	public class HeadquartersEventChannel : EventChannelBase
	{
		public EventChannel OnFirstTimeGameLoaded;
		public EventChannel DailyRewardAvailability;
	}
}
