/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About BasicAirMovementHandler
* -> Handles basic air movement like jumping, falling, landing
*/

public class BasicAirMovementHandler : MonoBehaviour
{
    private AnimatorHandler animHandler;
    private InputHandler inputHandler;
    private PlayerMovementBase pMoveBase;

    public void Init()
    {
        animHandler = GetComponent<AnimatorHandler>();
        inputHandler = animHandler.inputHandler;
        pMoveBase = animHandler.pMoveBase;
    }

    public void Tick()
    {
        
    }
}
