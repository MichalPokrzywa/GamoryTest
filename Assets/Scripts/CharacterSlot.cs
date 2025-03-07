using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    public enum SlotType
    {
        Helmet = 0,
        Armor = 1,
        Boots = 2,
        Necklace = 3,
        Ring = 4,
        Weapon = 5,
    }

    public GameObject slottedObject;
    public SlotType slotType;
    public Item slottedItem;

    public void AssignItemInSlot(GameObject item)
    {
        slottedObject = item;
        slottedObject.transform.SetParent(this.transform);
        slottedObject.transform.localPosition = Vector3.zero;
    }
}
