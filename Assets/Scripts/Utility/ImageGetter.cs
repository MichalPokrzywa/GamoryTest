using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGetter : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void RaycastDetect(bool toDetect)
    {
        image.raycastTarget = toDetect;
    }
}