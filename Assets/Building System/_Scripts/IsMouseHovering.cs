using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsMouseHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool _mouseOverUIElement;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseOverUIElement = true;
        Debug.Log("Mouse just entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseOverUIElement = false;
        Debug.Log("Mouse just exited");
    }
}
