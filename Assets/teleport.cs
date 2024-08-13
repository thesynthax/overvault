/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About teleport
* -> 
*/

public class teleport : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform controller;

    public void Teleport()
    {
        controller.position = spawnPoint.position;
    }
}
