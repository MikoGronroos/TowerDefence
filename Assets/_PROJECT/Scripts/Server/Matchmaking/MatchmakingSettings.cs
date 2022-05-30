using UnityEngine;

[CreateAssetMenu(menuName = "Matchmaking Settings", fileName = "New Matchmaking Settings")]
public class MatchmakingSettings : ScriptableObject
{
	public int MaxTrophyDifference;
	public int MaxPlayers;
	public bool Public;
}
