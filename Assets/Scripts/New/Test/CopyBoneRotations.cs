/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About CopyBoneRotations
* -> 
*/

public class CopyBoneRotations : MonoBehaviour
{
    public Transform bone;
    public bool mirror;

    private void Update()
    {
        if (!mirror)
            transform.rotation = bone.rotation;
        else
            transform.rotation = Quaternion.Inverse(bone.rotation);
    }
}
