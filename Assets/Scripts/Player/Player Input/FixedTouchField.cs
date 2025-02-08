using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour
{
    [SerializeField] InputEventsSO inputEventSO;

    private Vector2 touchDist;
    private Vector2 pointerOld;
    private bool pressed;
    private PointerEventData pointerEventData;

    void Update()
    {
        if (pressed)
        {
            touchDist = pointerEventData.position - pointerOld;
            pointerOld = pointerEventData.position;
        }
        else
        {
            touchDist = Vector2.zero;
        }
        inputEventSO.LookAroundEvent.Invoke(touchDist);
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
