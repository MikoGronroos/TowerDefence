using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/Barracks/Barrack Logic", fileName = "New Logic")]
public class BarrackLogic : ScriptableObject
{

    public GameObject UnitPrefab;

    public float SpawnInterval;
    public int SpawnAmount;

}
