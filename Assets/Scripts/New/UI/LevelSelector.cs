/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using UnityEngine.UIElements;

/** About LevelSelector
* -> Level Selector.
*/

public class LevelSelector : MonoBehaviour
{
    private Button all, basic, vault, slide, climb, air;
    private void Start()
    {
        all = transform.GetChild(0).GetComponent<Button>();
        basic = transform.GetChild(1).GetComponent<Button>();
        vault = transform.GetChild(2).GetComponent<Button>();
        slide = transform.GetChild(3).GetComponent<Button>();
        climb = transform.GetChild(4).GetComponent<Button>();
        air = transform.GetChild(5).GetComponent<Button>();
    }

    public void Teleport()
    {
        
    }
}
