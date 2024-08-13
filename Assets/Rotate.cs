/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About Rotate
* -> 
*/

public class Rotate : MonoBehaviour
{
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public float w = 1f;
      
    private void Start()
    {
        transform.rotation = Quaternion.identity;
        //Debug.Log(transform.rotation.x + " " + transform.rotation.y + " " + transform.rotation.z + " " + transform.rotation.w);
    }
    private void Update()
    {
        transform.rotation = new Quaternion(x, y, z, w);
    }
}
