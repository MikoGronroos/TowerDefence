using Finark.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/PlayerEventChannel", fileName ="PlayerEventChannel")]
public class PlayerEventChannel : EventChannelBase
{

	public EventChannel OnHealthChanged;

	public EventChannel OnPlayerLevelChanged;
	public EventChannel OnPlayerXPChanged;


}