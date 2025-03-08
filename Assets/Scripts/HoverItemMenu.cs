using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverItemMenu : MonoBehaviour
{
    [SerializeField] private float hoverTime;
    public IEnumerator StartHoverTimer(Item item)
    {
        yield return new WaitForSeconds(hoverTime);
        Debug.Log("Hovered for 1 second!");
        Debug.Log(item.Name);
        gameObject.SetActive(true);
        // Perform your action here (e.g., show item details)
    }
}
