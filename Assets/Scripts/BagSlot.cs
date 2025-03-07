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

    private void HandleItemRemoved(InventoryItem item)
    {
        slottedObject = null;
        slottedItem = null;
        item.GetComponent<ItemParentChange>().OnRemovedFromSlot -= HandleItemRemoved;
    }
}
