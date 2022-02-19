using UnityEngine;

namespace Finark.Events
{
	[CreateAssetMenu(menuName = "EventChannels/CameraEventChannel", fileName ="CameraEventChannel")]
	public class CameraEventChannel : EventChannelBase
	{

		public EventChannel AssignCameraToCanvas;

	}
}
