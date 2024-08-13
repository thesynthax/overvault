/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using UnityEngine.UI;

/** About ButtonsHandler
* -> 
*/

public class ButtonsHandler : MonoBehaviour
{
    private Toggle crouch;
    private Button slide;
    private Toggle sprint;
    public StateHandler states;
    public PlayerMovementBase pMoveBase;

    private void Start()
    {
        foreach (Transform t in transform.GetComponentInChildren<Transform>())
        {
            if (t.name == "Crouch")
                crouch = t.GetComponent<Toggle>();
            else if (t.name == "Slide")
                slide = t.GetComponent<Button>();
            else if (t.name == "Sprint")
                sprint = t.GetComponent<Toggle>();
        }
    }

    private void Update()
    {
        if (states.curState == 1 || states.curState == 2 || states.curState == 0)
        {
            crouch.interactable = true;
            slide.interactable = false;

            crouch.gameObject.SetActive(true);
            slide.gameObject.SetActive(false);
        }
        else if (states.curState == 3)
        {
            crouch.interactable = false;
            slide.interactable = true;

            crouch.gameObject.SetActive(false);
            slide.gameObject.SetActive(true);
        }
        else if (states.currentState != StateHandler.CurrentState.Crouching)
        {
            slide.interactable = false;
            crouch.interactable = false;
        }

        if (states.currentState == StateHandler.CurrentState.Crouching)
        {
            crouch.gameObject.SetActive(true);
            crouch.interactable = pMoveBase.slideCrouchHandler.UnderObstacleTime() > 0f ? false : true;
            slide.gameObject.SetActive(false);
            sprint.interactable = false;
        }
        else
        {
            sprint.interactable = true;
        }
    }
}
