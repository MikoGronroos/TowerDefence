using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/RoomEventChannel", fileName = "RoomEventChannel")]
	public class RoomEventChannel : EventChannelBase
	{

		public EventChannel LeaveRoom;
		public EventChannel OnJoinedRoom;

		public EventChannel OnPlayerAmountChanged;
		public EventChannel OnPlayerReadyUp;

		public EventChannel OnMapChanged;

		#region Settings

		public EventChannel OnPlayModeChanged;

        #endregion

    }
}
