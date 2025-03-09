using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverItemMenu : MonoBehaviour
{
    [SerializeField] private HoverItemUI hoverUI;
    [SerializeField] private float hoverTime;

    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            this.transform.position = Input.mousePosition;
        }
    }

    public IEnumerator StartHoverTimer(Item item)
    {
        yield return new WaitForSeconds(hoverTime);
        Debug.Log("Hovered for " + hoverTime + " second!");
        Debug.Log(item.Name);
        hoverUI.LoadItemOnUI(item);
        hoverUI.ModifyPivotBaseOnScreenPosition(Input.mousePosition);
        this.transform.position = Input.mousePosition;
        gameObject.SetActive(true);
    }
}