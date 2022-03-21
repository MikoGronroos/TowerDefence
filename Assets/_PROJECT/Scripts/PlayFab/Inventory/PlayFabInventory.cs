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

                foreach (var item in items)
                {

                    GameObject slot = Instantiate(itemUIPrefab, itemsParent);
                    
                    //slot.GetComponent<OwnedItemSlot>()?.SetData(item.CustomData["MainKey"], item.CustomData["SkinName"]);

                }
            },
            error => 
            {
                Debug.LogError("Couldn't load users inventory.");
            });

    }

}
