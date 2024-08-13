/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About TestCam
* -> 
*/

public class TestCam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float camMoveSpeed;
    private Vector3 velocity = Vector3.zero;


    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, camMoveSpeed);
        transform.position = smoothedPosition;
    }
}
