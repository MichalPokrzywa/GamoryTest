using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IPointerDownHandler
{
    private ItemLoader itemLoader = new ItemLoader();
    [Header("Base Elements of Inventory")]
    [SerializeField] private InventoryBag inventoryBagManager;
    [SerializeField] private CharacterInventory characterInventory;
    
    private GameObject pickedItem;
    private async void Start()
    {
        LoadingSpinner.OnLoadingStart.Invoke();
        StartCoroutine(inventoryBagManager.CreateItems(await itemLoader.FetchItems(),LoadingSpinner.OnLoadingEnd));
    }

    void Update()
    {
        if (pickedItem != null)
        {
            pickedItem.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedItem = eventData.pointerCurrentRaycast.gameObject;
            if (clickedItem == null) return;
            Debug.Log(clickedItem);

            if (pickedItem != null)
            {
                HandleItemPlacement(clickedItem);
            }
            else
            {
                PickUpItem(clickedItem);
            }
        }
    }

    private void HandleItemPlacement(GameObject clickedItem)
    {
        if (clickedItem.GetComponent<BagSlot>())
        {

            AssignItemToSlot(clickedItem.GetComponent<BagSlot>());
        }
        else if (clickedItem.GetComponent<CharacterSlot>() != null)
        {
            if (IsMatchingCategory(clickedItem.GetComponent<CharacterSlot>()))
            {
                AssignItemToSlot(clickedItem.GetComponent<CharacterSlot>());
            }
        }
        else if (clickedItem.GetComponent<InventoryItem>() != null)
        {
            if (clickedItem.transform.parent.GetComponent<BagSlot>() != null)
            {
                AssignItemToSlot(clickedItem.transform.parent.GetComponent<BagSlot>(), clickedItem);
            }
            else if (IsMatchingCategory(clickedItem.transform.parent.GetComponent<CharacterSlot>()))
            {
                AssignItemToSlot(clickedItem.transform.parent.GetComponent<CharacterSlot>(),clickedItem);
            }
        }
    }
    private void AssignItemToSlot(BagSlot slot, GameObject newPickedItem = null)
    {
        slot.AssignItemInSlot(pickedItem);
        pickedItem.GetComponent<Image>().raycastTarget = true;
        characterInventory.LightDownCategories();
        pickedItem = newPickedItem;
        if (newPickedItem != null)
        {
            PickUpItem(newPickedItem);
        }
    }

    private void AssignItemToSlot(CharacterSlot slot, GameObject newPickedItem = null)
    {
        slot.AssignItemInSlot(pickedItem);
        pickedItem.GetComponent<Image>().raycastTarget = true;
        characterInventory.LightDownCategories();
        pickedItem = newPickedItem;
        if (newPickedItem != null)
        {
            PickUpItem(newPickedItem);
        }
    }

    private void PickUpItem(GameObject clickedItem)
    {
        InventoryItem inventoryItem = clickedItem.GetComponent<InventoryItem>();
        if (inventoryItem != null)
        {
            pickedItem = clickedItem;
            pickedItem.GetComponent<Image>().raycastTarget = false;
            pickedItem.transform.SetParent(transform);
            characterInventory.LightUpCategory(pickedItem.GetComponent<InventoryItem>().GetItem().Category);
        }
    }

    private bool IsMatchingCategory(CharacterSlot slot)
    {
        return pickedItem.GetComponent<InventoryItem>().GetItem().Category == slot.slotType;
    }
}
