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

    public GameObject slottedItem;
    public SlotType slotType;


}
