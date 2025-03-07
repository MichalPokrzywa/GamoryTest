using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField] private Image imageSlot;
    public GameObject slottedObject;
    public string slotType;
    public Item slottedItem;

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

    private void HandleItemRemoved(InventoryItem item)
    {
        slottedObject = null;
        slottedItem = null;
        item.GetComponent<ItemParentChange>().OnRemovedFromSlot -= HandleItemRemoved;
        ShowSlot();
    }
}
