using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/UnitEventChannel", fileName ="UnitEventChannel")]
	public class UnitEventChannel : EventChannelBase
	{

		public EventChannel OnUnitReachedGoal;
		public EventChannel OnUnitKilled;

	}
}
