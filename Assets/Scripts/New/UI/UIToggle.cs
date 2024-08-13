/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using UnityEngine.UI;

/** About UIToggle
* -> Stays on every UI toggle
*/

public class UIToggle : MonoBehaviour
{
    public new string name;

    public enum CurrentPressState
    {
        On, Off
    }

    public CurrentPressState currentPressState;

    private void Update() 
    {
        currentPressState = GetComponent<Toggle>().isOn ? CurrentPressState.On : CurrentPressState.Off;

        Image img = GetComponent<Image>();
        img.color = currentPressState == CurrentPressState.On ? Color.grey : Color.white;
    }
}
