using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryBag : MonoBehaviour
{
    public Transform itemContainer;
    public GameObject itemPrefab;
    public CanvasGroup canvasGroup;
    public List<InventoryItem> itemsInInventory = new List<InventoryItem>();
    public IEnumerator CreateItems(ItemData itemData)
    {
        foreach (Item item in itemData.Items)
        {
            GameObject tmp = Instantiate(itemPrefab, itemContainer);
            InventoryItem tempItem = tmp.GetComponent<InventoryItem>();
            tempItem.AssignItem(item);
            tempItem.tableId = tempItem.transform.GetSiblingIndex();
            itemsInInventory.Add(tempItem);
        }

        yield return null;
    }

    public void AddItemToInventory(GameObject item)
    {
        item.transform.SetParent(itemContainer);
        item.transform.SetSiblingIndex(item.GetComponent<InventoryItem>().tableId);
    }

    public void TurnCanvasGroup(bool value)
    {
        canvasGroup.blocksRaycasts = value;
    }

    public void UpdateItemsIndexInTable()
    {
        foreach (InventoryItem item in itemsInInventory)
        {
            item.tableId = item.transform.GetSiblingIndex();
        }
    }

}
