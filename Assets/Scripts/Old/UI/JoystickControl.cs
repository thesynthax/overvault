/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/** About JoystickControl
* -> Handles all the Joystick control logic
*/

public class JoystickControl : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{   
    private Image bgImg;
    private Image joystickImg;
    private Vector3 inputVector;
    public enum CurrentPressState {Pressing, WasPressed, Released}
    public CurrentPressState currentPressState;
    private void Start()
    {
        bgImg = transform.GetChild(0).GetComponent<Image>();
        joystickImg = bgImg.transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
        currentPressState = CurrentPressState.Pressing;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        currentPressState = CurrentPressState.Released;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x*2 - 1, 0, pos.y*2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
        
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * bgImg.rectTransform.sizeDelta.x/3, inputVector.z * bgImg.rectTransform.sizeDelta.y/3);
        }
        currentPressState = CurrentPressState.Pressing;
    }

    //Deprecated
    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }


    public float GetHorizontal()
    {
        return inputVector.x;
    }

    public float GetVertical()
    {
        return inputVector.z;
    }
}
