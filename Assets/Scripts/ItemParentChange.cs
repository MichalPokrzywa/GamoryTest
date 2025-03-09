using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParentChange : MonoBehaviour
{
    public event Action<InventoryItem> OnRemovedFromSlot;

    private void OnTransformParentChanged()
    {
        if (transform.parent.GetComponent<CharacterSlot>() == null)
        {
            OnRemovedFromSlot?.Invoke(this.GetComponent<InventoryItem>());
        }
        else if (transform.parent.GetComponent<BagSlot>() == null)
        {
            OnRemovedFromSlot?.Invoke(this.GetComponent<InventoryItem>());
        }
    }
}
