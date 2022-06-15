using UnityEngine;

public class UnitEffect : ScriptableObject
{
	[field: SerializeField] public string effectId { get; set; }
	[field: SerializeField] public GameObject effectPrefab { get; private set; }
	[field: SerializeField] public float effectDuration { get; private set; }

	public virtual void StartEffect(Unit unit) { }

	public virtual void StopEffect(Unit unit) { }

}
