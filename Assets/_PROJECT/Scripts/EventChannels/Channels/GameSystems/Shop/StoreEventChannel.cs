using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/StoreEventChannel", fileName ="StoreEventChannel")]
	public class StoreEventChannel : EventChannelBase
	{
		public EventChannel StoreItemsFetched;
		public EventChannel BundleFetched;
		public EventChannel OnStoreOpened;
	}
}
