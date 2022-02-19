using Finark.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/PlayerEventChannel", fileName ="PlayerEventChannel")]
public class PlayerEventChannel : EventChannelBase
{

	public EventChannel OnHealthChanged;

	public EventChannel OnPlayerLevelChanged;
	public EventChannel OnPlayerXPChanged;

	public EventChannel OnPlayerCurrencyChanged;
	public EventChannel OnPlayerCurrencyIncomeChanged;
	public EventChannel OnPlayerCurrencyIntervalUpdate;
}