using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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

    private void Update()
    {
        if (pickedItem != null)
        {
            pickedItem.transform.position = Input.mousePosition;
        }
    }

    #region Pointer Methods

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            HandleLeftClick(eventData.pointerCurrentRaycast.gameObject);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            HandleRightClick(eventData.pointerCurrentRaycast.gameObject);
        }
    }

    private void HandleLeftClick(GameObject clickedItem)
    {
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

    private void HandleRightClick(GameObject clickedItem)
    {
        if (pickedItem != null || clickedItem == null) return;

        InventoryItem inventoryItem = clickedItem.GetComponent<InventoryItem>();
        if (inventoryItem == null) return;

        if (clickedItem.transform.parent.TryGetComponent(out BagSlot bagSlot))
        {
            FastMoveItemsBetweenSlots(clickedItem);
        }
        else if (clickedItem.transform.parent.TryGetComponent(out CharacterSlot characterSlot))
        {
            inventoryBagManager.GetEmptyBagSlot()?.AssignItemInSlot(clickedItem);
        }

        characterInventory.LightDownCategories();
        OnPointerExit(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (pickedItem != null) return;

        GameObject hoveredItem = eventData.pointerCurrentRaycast.gameObject;
        if (hoveredItem == null || hoveredItem.GetComponent<InventoryItem>() == null) return;

        Debug.Log(hoveredItem);
        hoverItemCoroutine = StartCoroutine(hoverItemMenu.StartHoverTimer(hoveredItem.GetComponent<InventoryItem>().GetItem()));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverItemCoroutine == null) return;

        StopCoroutine(hoverItemCoroutine);
        hoverItemMenu.gameObject.SetActive(false);
        hoverItemCoroutine = null;
    }

    #endregion

    #region Moving Items

    private void HandleItemPlacement(GameObject clickedItem)
    {
        if (clickedItem.TryGetComponent(out BagSlot bagSlot))
        {
            AssignItemToBagSlot(bagSlot);
        }
        else if (clickedItem.TryGetComponent(out CharacterSlot characterSlot) && IsMatchingCategory(characterSlot))
        {
            AssignItemToCharacterSlot(characterSlot);
        }
        else if (clickedItem.TryGetComponent(out InventoryItem inventoryItem))
        {
            Transform parent = clickedItem.transform.parent;
            if (parent.TryGetComponent(out BagSlot parentBagSlot))
            {
                AssignItemToBagSlot(parentBagSlot, clickedItem);
            }
            else if (parent.TryGetComponent(out CharacterSlot parentCharacterSlot) && IsMatchingCategory(parentCharacterSlot))
            {
                AssignItemToCharacterSlot(parentCharacterSlot, clickedItem);
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
        pickedItem?.GetComponent<ImageGetter>().RaycastDetect(true);
        characterInventory.LightDownCategories();
        pickedItem = newPickedItem;
        if (pickedItem != null)
        {
            PickUpItem(pickedItem);
        }
    }

    private void PickUpItem(GameObject clickedItem)
    {
        InventoryItem inventoryItem = clickedItem.GetComponent<InventoryItem>();
        if (inventoryItem == null) return;

        pickedItem = clickedItem;
        pickedItem.GetComponent<ImageGetter>().RaycastDetect(false);
        pickedItem.transform.SetParent(transform);
        characterInventory.LightUpCategory(inventoryItem.GetItem().Category);
        OnPointerExit(null);
    }

    private void FastMoveItemsBetweenSlots(GameObject clickedItem)
    {
        InventoryItem inventoryItem = clickedItem.GetComponent<InventoryItem>();
        if (inventoryItem == null) return;

        CharacterSlot slot = characterInventory.GetCharacterSlotByCategory(inventoryItem.GetItem().Category);
        if (slot == null) return;

        if (slot.slottedObject == null)
        {
            slot.AssignItemInSlot(clickedItem);
        }
        else
        {
            BagSlot bagSlot = clickedItem.transform.parent.GetComponent<BagSlot>();
            GameObject tmp = slot.slottedObject;
            slot.AssignItemInSlot(clickedItem);
            bagSlot.AssignItemInSlot(tmp);
        }
    }

    #endregion

    public void SendStatsToPlayer()
    {
        GameManager.Instance.StartGameplay(characterInventory.GetStats());
    }

    private bool IsMatchingCategory(CharacterSlot slot)
    {
        return pickedItem?.GetComponent<InventoryItem>().GetItem().Category == slot.slotType;
    }

    private IEnumerator LoadItemsCoroutine()
    {
        LoadingSpinner.OnLoadingStart?.Invoke();

        var task = itemLoader.FetchItems();
        yield return new WaitUntil(() => task.IsCompleted);

        StartCoroutine(inventoryBagManager.CreateItems(task.Result, LoadingSpinner.OnLoadingEnd));
    }
}
