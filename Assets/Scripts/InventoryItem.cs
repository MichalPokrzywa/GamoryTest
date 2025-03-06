using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemRarity;
    private Item item;

    public void AssignItem(Item CommingItem)
    {
        item = CommingItem;
        itemIcon.sprite = item.Icon;
        itemRarity.sprite = RarityLoader.GetTextureToLoad(item.Rarity);
    }
}
