using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour
{
    [SerializeField] ReferencesSO referencesSO;

    private Vector2 swipeDistance;
    private Vector2 pointerOld;
    private bool pressed;
    private PointerEventData pointerEventData;

    void Update()
    {
        if (pressed)
        {
            swipeDistance = pointerEventData.position - pointerOld;
            pointerOld = pointerEventData.position;
            if(referencesSO != null)
                referencesSO.SwipeDistance = swipeDistance;
        }
        else
        {
            swipeDistance = Vector2.zero;
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
