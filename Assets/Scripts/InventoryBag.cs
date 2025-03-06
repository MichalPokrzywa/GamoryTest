using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InventoryBag : MonoBehaviour
{
    public Transform itemContainer;
    public GameObject itemPrefab;

    public IEnumerator CreateItems(ItemData itemData)
    {
        foreach (Item item in itemData.Items)
        {
            GameObject tmp = Instantiate(itemPrefab, itemContainer);
            tmp.GetComponent<InventoryItem>().AssignItem(item);
        }

        yield return null;
    } 

}
