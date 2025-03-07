using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSlot : MonoBehaviour
{
    public GameObject slottedObject;
    public Item slottedItem;
    public int tableId;

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
    }

    public bool HasItem()
    {
        if (slottedItem != null)
            return true;
        return false;
    }

    public Item GetItem()
    {
        return slottedItem;
    }

    private void HandleItemRemoved(InventoryItem item)
    {
        slottedObject = null;
        slottedItem = null;
        item.GetComponent<ItemParentChange>().OnRemovedFromSlot -= HandleItemRemoved;
    }

}
