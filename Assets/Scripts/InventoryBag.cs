using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<BagSlot> slotsInInventory = new List<BagSlot>();

    public IEnumerator CreateItems(ItemData itemData, UnityAction onComplete)
    {
        foreach (Item item in itemData.Items)
        {
            GameObject slotObject = Instantiate(slotPrefab, itemContainer);
            GameObject itemObject = Instantiate(itemPrefab, slotObject.transform);
            InventoryItem tempItem = itemObject.GetComponent<InventoryItem>();
            BagSlot tempSlot = slotObject.GetComponent<BagSlot>();
            tempItem.AssignItem(item);
            tempSlot.AssignItemInSlot(itemObject);
            slotsInInventory.Add(tempSlot);
            yield return null;
        }

        sorter.Initialize(slotsInInventory);
        onComplete.Invoke();
    }

    public List<BagSlot> GetSlotList()
    {
        return slotsInInventory;
    }

    public BagSlot GetEmptyBagSlot()
    {
        return slotsInInventory.First(slot => slot.slottedObject == null);
    }
}