/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About Parent
* -> 
*/

public class Parent
{
    protected int x;
    private void assign()
    {
        x = 5;
    }
    private void Start()
    {
        assign();
        Debug.Log(x);
    }
}
