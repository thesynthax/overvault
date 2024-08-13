/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using UnityEngine.EventSystems;

/** About crouch
* -> 
*/

public class crouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed { get; set; }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        pressed = true;
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        pressed = false;
    }
}
