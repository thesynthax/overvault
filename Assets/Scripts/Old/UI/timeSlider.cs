/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using UnityEngine.UI;

/** About timeSlider
* -> Scales time
*/

public class timeSlider : MonoBehaviour
{
    private Slider slider;

    private void Start() 
    {
        slider = GetComponentInChildren<Slider>();    
    }

    private void FixedUpdate() 
    {
        Time.timeScale = slider.value;    
    }
}
