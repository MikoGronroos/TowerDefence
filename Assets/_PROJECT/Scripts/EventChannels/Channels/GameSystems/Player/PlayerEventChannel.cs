using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/PlayerEventChannel", fileName = "PlayerEventChannel")]
	public class PlayerEventChannel : EventChannelBase
	{

		public EventChannel OnHealthChanged;

		public EventChannel OnPlayerLevelUp;
		public EventChannel OnPlayerXPChanged;

		public EventChannel OnPlayerDead;

		public EventChannel RefreshMissionLog;
		public EventChannel OnMissionCompleted;

	}
}