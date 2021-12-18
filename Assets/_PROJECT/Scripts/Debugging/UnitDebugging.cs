using UnityEngine;
using PathCreation;

public class UnitDebugging : MonoBehaviour
{

    [SerializeField] private GameObject unitPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UnitSpawner.Instance.SpawnUnit(unitPrefab.name, 0);
        }
    }


}
