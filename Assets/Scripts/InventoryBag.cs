using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class InventoryBag : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject itemPrefab;
    public GameObject slotPrefab;
    public Transform itemContainer;
    [Header("Inventory Sorter")]
    public BagSorter sorter;
    private List<InventoryItem> itemsInInventory = new List<InventoryItem>();
    private List<BagSlot> slotsInInventory = new List<BagSlot>();
    public IEnumerator CreateItems(ItemData itemData,UnityAction onComplete)
    {
        foreach (Item item in itemData.Items)
        {
            GameObject slotObject = Instantiate(slotPrefab, itemContainer);
            GameObject itemObject = Instantiate(itemPrefab, slotObject.transform);
            InventoryItem tempItem = itemObject.GetComponent<InventoryItem>();
            BagSlot tempSlot = slotObject.GetComponent<BagSlot>();
            tempItem.AssignItem(item);
            slotsInInventory.Add(tempSlot);
            slotObject.GetComponent<BagSlot>().AssignItemInSlot(itemObject);
            itemsInInventory.Add(tempItem);
            yield return null;
        }

        sorter.Initialize(slotsInInventory);
        onComplete.Invoke();
    }

    public List<BagSlot> GetSlotList()
    {
        return slotsInInventory;
    }

}
