using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HoverItemUI : MonoBehaviour
{
    [Header("UI Elements")] [SerializeField]
    private Image itemIcon;

    [SerializeField] private Image itemBackground;
    [SerializeField] private Image itemNameBackground;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemHp;
    [SerializeField] private TMP_Text itemDamage;
    [SerializeField] private TMP_Text itemAttackSpeed;
    [SerializeField] private TMP_Text itemCritChance;
    [SerializeField] private TMP_Text itemMoveSpeed;

    public void LoadItemOnUI(Item item)
    {
        itemIcon.sprite = item.Icon;
        itemBackground.sprite = RarityLoader.GetTextureIconToLoad(item.Rarity);
        itemNameBackground.sprite = RarityLoader.GetTextureBackgroundToLoad(item.Rarity);
        itemName.text = item.Name;
        itemHp.text = item.HealthPoints.ToString();
        itemDamage.text = item.Damage.ToString();
        itemAttackSpeed.text = item.AttackSpeed.ToString("0.00") + " %";
        itemCritChance.text = item.CriticalStrikeChance.ToString("0.00") + " %";
        itemMoveSpeed.text = item.MovementSpeed.ToString("0.00") + " %";
    }

    public void ModifyPivotBaseOnScreenPosition(Vector2 screenPosition)
    {
        // Determine the screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Determine which quadrant the mouse is in
        RectTransform interactionMenuRect = this.GetComponent<RectTransform>();

        if (screenPosition.x < screenWidth / 2 && screenPosition.y > screenHeight / 2)
        {
            // Top Left
            interactionMenuRect.pivot = Vector2.up;
        }
        else if (screenPosition.x > screenWidth / 2 && screenPosition.y > screenHeight / 2)
        {
            // Top Right
            interactionMenuRect.pivot = Vector2.one;
        }
        else if (screenPosition.x < screenWidth / 2 && screenPosition.y < screenHeight / 2)
        {
            // Bottom Left
            interactionMenuRect.pivot = Vector2.zero;
        }
        else if (screenPosition.x > screenWidth / 2 && screenPosition.y < screenHeight / 2)
        {
            // Bottom Right
            interactionMenuRect.pivot = Vector2.right;
        }
    }
}