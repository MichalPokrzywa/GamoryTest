using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public List<CharacterSlot> itemSlots = new List<CharacterSlot>();

    public void LightUpCategory(string Catergory)
    {
        foreach (CharacterSlot slot in itemSlots)
        {
            if (slot.slotType.Equals(Catergory))
            {
                slot.ShowSlot();
            }
        }
    }

    public void LightDownCategories()
    {
        foreach (CharacterSlot slot in itemSlots)
        {
            slot.HideSlot();
        }
    }
}
