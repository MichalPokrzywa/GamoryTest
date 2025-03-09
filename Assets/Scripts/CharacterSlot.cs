using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] private Image imageSlot;
    public GameObject slottedObject;
    public string slotType;
    public Item slottedItem;
    public UnityAction OnItemSlotChange;
    public void AssignItemInSlot(GameObject item)
    {
        slottedObject = item;
        slottedItem = item.GetComponent<InventoryItem>().GetItem();
        slottedObject.transform.SetParent(this.transform);
        slottedObject.transform.localPosition = Vector3.zero;
        var slotItem = slottedObject.GetComponent<ItemParentChange>();
        if (slotItem != null)
        {
            slotItem.OnRemovedFromSlot += HandleItemRemoved;
        }
        OnItemSlotChange.Invoke();
        HideSlot();
    }

    public void ShowSlot()
    {
        imageSlot.color = Color.cyan;
    }

    public void HideSlot()
    {
        imageSlot.color = Color.white;
    }
    public Item GetItem()
    {
        return slottedItem;
    }

    private void HandleItemRemoved(InventoryItem item)
    {
        if (slottedItem == item.GetItem())
        {
            slottedItem = null;
            slottedObject = null;
        }
        item.GetComponent<ItemParentChange>().OnRemovedFromSlot -= HandleItemRemoved;
        OnItemSlotChange.Invoke();
        ShowSlot();
    }
}
