using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryBag : MonoBehaviour
{
    public Transform itemContainer;
    public GameObject itemPrefab;
    public GameObject slotPrefab;
    public CanvasGroup canvasGroup;
    public List<InventoryItem> itemsInInventory = new List<InventoryItem>();
    public IEnumerator CreateItems(ItemData itemData)
    {
        foreach (Item item in itemData.Items)
        {
            GameObject slotObject = Instantiate(slotPrefab, itemContainer);
            GameObject itemObject = Instantiate(itemPrefab, slotObject.transform);
            InventoryItem tempItem = itemObject.GetComponent<InventoryItem>();
            tempItem.AssignItem(item);
            //tempItem.tableId = tempItem.transform.GetSiblingIndex();
            slotObject.GetComponent<BagSlot>().AssignItemInSlot(itemObject);
            itemsInInventory.Add(tempItem);
        }

        yield return null;
    }

    public void AddItemToInventory(GameObject item)
    {
        item.transform.SetParent(itemContainer);
        //item.transform.SetSiblingIndex(item.GetComponent<InventoryItem>().tableId);
    }

    public void TurnCanvasGroup(bool value)
    {
        canvasGroup.blocksRaycasts = value;
    }

    public void UpdateItemsIndexInTable()
    {
        foreach (InventoryItem item in itemsInInventory)
        {
            //item.tableId = item.transform.GetSiblingIndex();
        }
    }

}
