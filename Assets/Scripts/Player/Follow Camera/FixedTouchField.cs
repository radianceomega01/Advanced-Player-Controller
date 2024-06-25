using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour
{
    public Vector2 TouchDist { get; private set; }

    private Vector2 pointerOld;
    private bool pressed;
    private PointerEventData pointerEventData;

    void Update()
    {
        if (pressed)
        {
            TouchDist = pointerEventData.position - pointerOld;
            pointerOld = pointerEventData.position;
        }
        else
        {
            TouchDist = Vector2.zero;
        }
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        pressed = true;
        pointerEventData = eventData as PointerEventData;
        pointerOld = pointerEventData.position;
    }


    public void OnPointerUp()
    {
        pressed = false;
    }
}
