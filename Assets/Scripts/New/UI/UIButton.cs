/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using UnityEngine.EventSystems;

/** About UIButton
* -> Stays on every UI button
*/

public class UIButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public new string name;

    public enum CurrentPressState
    {
        Pressing, Released, WasPressed
    }

    public CurrentPressState currentPressState;

    private void Start() 
    {
        currentPressState = CurrentPressState.Released;    
    }

    public virtual void OnPointerClick(PointerEventData ped)
    {
        currentPressState = CurrentPressState.WasPressed;
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        currentPressState = CurrentPressState.Pressing;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        currentPressState = CurrentPressState.Released;
    }
}
