/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About testsc
* -> 
*/

public class testsc : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        Debug.Log(GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).right); 
    }
}
