using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/ShopEventChannel", fileName ="ShopEventChannel")]
	public class ShopEventChannel : EventChannelBase
	{

		public EventChannel RefreshShop;
		public EventChannel OnEnteredDragging;
		public EventChannel OnExitedDragging;

	}
}
