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
        StartCoroutine(inventoryBagManager.CreateItems(await itemLoader.FetchItems()));
        LoadingSpinner.OnLoadingEnd.Invoke();
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
            Debug.Log(clickedItem);

            if (pickedItem != null)
            {
                if (clickedItem.GetComponent<BagSlot>())
                {
                    clickedItem.GetComponent<BagSlot>().AssignItemInSlot(pickedItem);
                    pickedItem.GetComponent<Image>().raycastTarget = true;
                    pickedItem = null;
                    characterInventory.LightDownCategories();
                }
                else if (clickedItem.GetComponent<CharacterSlot>() != null)
                {
                    if (clickedItem.GetComponent<CharacterSlot>().slotType ==
                        pickedItem.GetComponent<InventoryItem>().GetItem().Category)
                    {
                        clickedItem.GetComponent<CharacterSlot>().AssignItemInSlot(pickedItem);
                        pickedItem.GetComponent<Image>().raycastTarget = true;
                        pickedItem = null;
                    }
                }
            }
            else
            {
                if (clickedItem.GetComponent<InventoryItem>() != null)
                {
                    pickedItem = clickedItem;
                    pickedItem.GetComponent<Image>().raycastTarget = false;
                    pickedItem.transform.SetParent(this.transform);
                    characterInventory.LightUpCategory(pickedItem.GetComponent<InventoryItem>().GetItem().Category);

                }
            }

        }
    }
}
