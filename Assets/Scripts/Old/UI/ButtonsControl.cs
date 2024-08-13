/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/** About ButtonsControl
* -> Handles all the UI Buttons logic 
*/

public class ButtonsControl : MonoBehaviour
{
    public StateManager stateMgr;
    private GameObject sprintButton;
    private GameObject jumpButton;
    private GameObject slideButton;
    private GameObject crouchButton;

    [HideInInspector] public jump jumpScript;
    [HideInInspector] public slide slideScript;
    [HideInInspector] public crouch crouchScript;

    private void Start()
    {
        foreach (Transform child in transform.GetComponentInChildren<Transform>())
        {
            if (child.name.Equals("Sprint"))
            {
                sprintButton = child.gameObject;
            }
            if (child.name.Equals("Jump"))
            {
                jumpButton = child.gameObject;
                jumpScript = jumpButton.GetComponent<jump>();
            }
            if (child.name.Equals("Slide"))
            {
                slideButton = child.gameObject;
                slideScript = slideButton.GetComponent<slide>();
            }
            if (child.name.Equals("Crouch"))
            {
                crouchButton = child.gameObject;
                crouchScript = crouchButton.GetComponent<crouch>();
            }
        }
    }

    public bool Sprint()
    {
        Toggle sprintToggle = sprintButton.GetComponent<Toggle>();
        bool pressed = sprintToggle.isOn || Input.GetKey(KeyCode.LeftShift);

        Image sprintButtonImage = sprintButton.GetComponent<Image>();
        sprintButtonImage.color = pressed ? Color.grey : Color.white;

        return pressed; 
    }

    private void Update()
    {
        if (stateMgr.charStates.curState == 0 || stateMgr.charStates.curState == 1 || stateMgr.charStates.curState == 2)
        {
            slideButton.GetComponent<Button>().interactable = true;
            crouchButton.GetComponent<Button>().interactable = true;

            slideButton.SetActive(false);
            crouchButton.SetActive(true);
        }
        else if (stateMgr.charStates.curState == 3)
        {
            slideButton.GetComponent<Button>().interactable = true;
            crouchButton.GetComponent<Button>().interactable = true;

            slideButton.SetActive(true);
            crouchButton.SetActive(false);
        }
        else
        {
            slideButton.GetComponent<Button>().interactable = false;
        }
    }
}
