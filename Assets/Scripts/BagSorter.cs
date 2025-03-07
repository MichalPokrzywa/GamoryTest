using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BagSorter : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private SortButton sortUpButton;
    [SerializeField] private SortButton sortDownButton;
    [SerializeField] private SortButton showAllButton;
    [SerializeField] private List<SortButton> categoryButtons;

    private List<BagSlot> itemSlots;

    public void Initialize(List<BagSlot> createdSlots)
    {
        sortUpButton.onClick.AddListener(SortUp);
        sortDownButton.onClick.AddListener(SortDown);
        showAllButton.onClick.AddListener(ShowAll);

        foreach (SortButton button in categoryButtons)
        {
            string category = button.category;
            button.onClick.AddListener(() => ShowCategory(category));
        }

        itemSlots = createdSlots;

    }

    public void SortUp()
    {
        // Items first, empty slots at bottom
        itemSlots = itemSlots
            .OrderByDescending(slot => slot.HasItem() ? slot.GetItem().Rarity : -1) 
            .ToList();

        UpdateInventoryUI();
    }

    public void SortDown()
    {
        // Items first, empty slots at bottom
        itemSlots = itemSlots
            .OrderBy(slot => slot.HasItem() ? slot.GetItem().Rarity : int.MaxValue) 
            .ToList();

        UpdateInventoryUI();
    }

    public void ShowAll()
    {
        foreach (var slot in itemSlots)
        {
            slot.gameObject.SetActive(true);
        }

        UpdateInventoryUI();
    }

    public void ShowCategory(string category)
    {
        foreach (var slot in itemSlots)
        {
            bool shouldShow = slot.HasItem() && slot.GetItem().Category == category;
            // Show empty slots too
            slot.gameObject.SetActive(shouldShow || !slot.HasItem()); 
        }

        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        SortSlotsWithEmptyAtBottom();
    }

    private void SortSlotsWithEmptyAtBottom()
    {
        // Move non-empty slots first, then empty slots at the bottom
        int index = 0;
        foreach (var slot in itemSlots.Where(slot => slot.HasItem()))
        {
            slot.transform.SetSiblingIndex(index++);
        }
        foreach (var slot in itemSlots.Where(slot => !slot.HasItem()))
        {
            slot.transform.SetSiblingIndex(index++);
        }
    }
}
