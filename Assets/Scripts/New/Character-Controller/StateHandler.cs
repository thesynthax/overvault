/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About StateHandler
* -> Contains all the state variables
*/

[CreateAssetMenu(fileName = "States Handler/States")]
public class StateHandler : ScriptableObject
{
    public enum CurrentState
    {
        Idle, Walking, Jogging, Sprinting, Jumping, Falling, Landing, Rolling, Vaulting, Crouching, Sliding, Climbing, Ledge, Ragdolled
    }
    public CurrentState currentState = new CurrentState();
    public int curState = 0;
    public bool onGround = false;
    public int facingDir = 0;
}
