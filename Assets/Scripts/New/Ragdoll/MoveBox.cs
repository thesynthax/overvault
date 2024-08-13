/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About MoveBox
* -> 
*/

public class MoveBox : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(-transform.right * 3 * Time.fixedDeltaTime); 
    }
}
