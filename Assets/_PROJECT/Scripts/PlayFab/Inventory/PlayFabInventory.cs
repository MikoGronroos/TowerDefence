using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayFabInventory : MonoBehaviour
{

    [SerializeField] private GameObject itemUIPrefab;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private Button loadItemsButton;

    private List<GameObject> _items = new List<GameObject>();

    private void Awake()
    {
        loadItemsButton.onClick.AddListener(LoadInventory);
    }

    public void LoadInventory()
    {

        GetUserInventoryRequest request = new GetUserInventoryRequest();

        PlayFabClientAPI.GetUserInventory(request, 
            result =>
            {
                List<ItemInstance> items = result.Inventory;

                ClearInventoryUI();

                foreach (var item in items)
                {

                    GameObject slot = Instantiate(itemUIPrefab, itemsParent);
                    
                    slot.GetComponent<OwnedItemSlot>()?.SetData(item.CustomData["MainKey"], item.CustomData["SkinId"]);

                    _items.Add(slot);

                }
            },
            error => 
            {
                Debug.LogError("Couldn't load users inventory.");
            });

    }

    private void ClearInventoryUI()
    {
        foreach (var item in _items)
        {
            Destroy(item);
        }
        _items.Clear();
    }

}
