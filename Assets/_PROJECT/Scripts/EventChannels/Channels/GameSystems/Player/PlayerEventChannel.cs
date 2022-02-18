using Finark.Events;

public class PlayerEventChannel : EventChannelBase
{

	public readonly EventChannel OnHealthChanged;

	public readonly EventChannel OnPlayerLevelChanged;
	public readonly EventChannel OnPlayerXPChanged;


}