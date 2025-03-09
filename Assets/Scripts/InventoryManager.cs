using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private ItemLoader itemLoader = new ItemLoader();
    [Header("Base Elements of Inventory")]
    [SerializeField] private InventoryBag inventoryBagManager;
    [SerializeField] private CharacterInventory characterInventory;
    [SerializeField] private HoverItemMenu hoverItemMenu;
    
    private GameObject pickedItem;
    private Coroutine hoverItemCoroutine;
    private void Start()
    {
        StartCoroutine(LoadItemsCoroutine());
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void Update()
    {
        if (pickedItem != null)
        {
            pickedItem.transform.position = Input.mousePosition;
        }
    }

    #region PointerMethods

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
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(pickedItem != null) return;

        GameObject clickedItem = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(clickedItem);
        if (clickedItem.GetComponent<InventoryItem>() != null)
        {
            hoverItemCoroutine = StartCoroutine(hoverItemMenu.StartHoverTimer(clickedItem.GetComponent<InventoryItem>().GetItem()));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverItemCoroutine != null)
        {
            StopCoroutine(hoverItemCoroutine);
            hoverItemMenu.gameObject.SetActive(false);
            hoverItemCoroutine = null;
        }
    }
    #endregion
    #region MovingItems

    private void HandleItemPlacement(GameObject clickedItem)
    {
        if (clickedItem.GetComponent<BagSlot>())
        {
            AssignItemToBagSlot(clickedItem.GetComponent<BagSlot>());
        }
        else if (clickedItem.GetComponent<CharacterSlot>() != null)
        {
            if (IsMatchingCategory(clickedItem.GetComponent<CharacterSlot>()))
            {
                AssignItemToCharacterSlot(clickedItem.GetComponent<CharacterSlot>());
            }
        }
        else if (clickedItem.GetComponent<InventoryItem>() != null)
        {
            if (clickedItem.transform.parent.GetComponent<BagSlot>() != null)
            {
                AssignItemToBagSlot(clickedItem.transform.parent.GetComponent<BagSlot>(), clickedItem);
            }
            else if (IsMatchingCategory(clickedItem.transform.parent.GetComponent<CharacterSlot>()))
            {
                AssignItemToCharacterSlot(clickedItem.transform.parent.GetComponent<CharacterSlot>(),clickedItem);
            }
        }
    }
    private void AssignItemToBagSlot(BagSlot slot, GameObject newPickedItem = null)
    {
        slot.AssignItemInSlot(pickedItem);
        UpdatePickedItem(newPickedItem);
    }

    private void AssignItemToCharacterSlot(CharacterSlot slot, GameObject newPickedItem = null)
    {
        slot.AssignItemInSlot(pickedItem);
        UpdatePickedItem(newPickedItem);
    }

    private void UpdatePickedItem(GameObject newPickedItem)
    {
        pickedItem.GetComponent<ImageGetter>().RaycastDetect(true);
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
            pickedItem.GetComponent<ImageGetter>().RaycastDetect(false);
            pickedItem.transform.SetParent(transform);
            characterInventory.LightUpCategory(pickedItem.GetComponent<InventoryItem>().GetItem().Category);
            OnPointerExit(null);
        }
    }
    #endregion

    public void SendStatsToPlayer()
    {
        GameManager.Instance.StartGameplay(characterInventory.GetStats());
    }
    private bool IsMatchingCategory(CharacterSlot slot)
    {
        return pickedItem.GetComponent<InventoryItem>().GetItem().Category == slot.slotType;
    }

    private IEnumerator LoadItemsCoroutine()
    {
        LoadingSpinner.OnLoadingStart?.Invoke();

        var task = itemLoader.FetchItems();
        yield return new WaitUntil(() => task.IsCompleted);

        StartCoroutine(inventoryBagManager.CreateItems(task.Result, LoadingSpinner.OnLoadingEnd));
    }

}
