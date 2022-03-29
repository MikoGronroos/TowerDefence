using Finark.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SacrificialAltar : MonoBehaviour
{

	[SerializeField] private int currentAmountOfGold;
	[SerializeField] private int neededAmountOfGold;

    [SerializeField] private UnitEventChannel unitEventChannel;

    private HashSet<int> _unitInstanceIds = new HashSet<int>();

    private void OnEnable()
    {
        unitEventChannel.OnUnitKilled += UnitKilled;
    }

    private void OnDisable()
    {
        unitEventChannel.OnUnitKilled -= UnitKilled;
    }

    private void UnitKilled(Dictionary<string, object> args, Action<Dictionary<string, object>> callback)
    {
        var id = (int)args["InstanceID"];

        if (UnitIsInsideAltar(id))
        {
            Debug.Log("Was Killed Inside Altar!");
        }

    }

    public void AddGold(int amount)
    {
		currentAmountOfGold = Mathf.Clamp(currentAmountOfGold + amount, 0, neededAmountOfGold);
        if (CheckGold())
        {

        }
    }

	private bool CheckGold()
    {
        if (currentAmountOfGold >= neededAmountOfGold)
        {
            return true;
        }
        return false;
    }

    private bool UnitIsInsideAltar(int id)
    {
        return _unitInstanceIds.Contains(id);
    }

    private void AddUnitInstanceIDToList(int id)
    {
        _unitInstanceIds.Add(id);
    }

    private void RemoveUnitInstanceIDFromList(int id)
    {
        _unitInstanceIds.Remove(id);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {
            Debug.Log("Unit Entered The Altar!");
            AddUnitInstanceIDToList(unit.UnitInstanceId);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {
            Debug.Log("Unit Exited The Altar. :(");
            RemoveUnitInstanceIDFromList(unit.UnitInstanceId);
        }
    }

}
