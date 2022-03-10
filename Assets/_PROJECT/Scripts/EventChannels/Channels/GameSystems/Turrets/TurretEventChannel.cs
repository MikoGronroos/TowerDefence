using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/TurretEventChannel", fileName ="TurretEventChannel")]
	public class TurretEventChannel : EventChannelBase
	{

		public EventChannel OnTurretSelected;
		public EventChannel OnTurretUpgraded;

	}
}
