/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About CharacterStates
* -> Contains all variables related to the states of character.
*/

[CreateAssetMenu(fileName = "Character States")]
public class CharacterStates : ScriptableObject
{
    public int curState = 0;
    public bool onGround;
}
