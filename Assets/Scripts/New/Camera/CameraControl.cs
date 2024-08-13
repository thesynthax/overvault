/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About CameraControl
* -> Camera Movement
*/

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float camMoveSpeed;
    public PlayerMovementBase pMoveBase;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private UnityEngine.UI.Toggle camtoggle;

    private Transform camPivot;
    private void Start()
    {
        camPivot = target;
    }

    private void LateUpdate()
    {
        if (!camtoggle.isOn)
        {
            offset = new Vector3(1.5f, 0.5f, -6);
            transform.rotation = Quaternion.identity;
            if (pMoveBase.ragdollControl.ragdollState == RagdollControl.RagdollState.ragdolled)
            {
                offset.y = 0.5f;

                target = pMoveBase.anim.GetBoneTransform(HumanBodyBones.Hips);

                float difference = 0f;
                if (target.position.y > pMoveBase.ragdollControl.hipY)
                {
                    difference = target.position.y - pMoveBase.ragdollControl.hipY;
                }
                else if (target.position.y < (pMoveBase.ragdollControl.hipY - 0.85f))
                {
                    difference = -(pMoveBase.ragdollControl.hipY - 0.85f - target.position.y);
                }
                offset.y += difference;

                Vector3 desiredPosition = new Vector3(target.position.x, camPivot.position.y, target.position.z) + offset;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, camMoveSpeed);
                transform.position = smoothedPosition;
            }
            else
            {
                if (pMoveBase.slideCrouchHandler.UnderObstacleTime() < 0.1f)
                {
                    offset.y = 0.5f;
                }
                else
                {
                    offset.y = -0.3f;
                }
                target = camPivot;
                Vector3 desiredPosition = target.position + offset;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, camMoveSpeed);
                transform.position = smoothedPosition;
            }
        }
        else
        {
            offset = new Vector3(3.5f, 5, -11);
            Vector3 desiredPosition = target.position + offset;
            transform.position = desiredPosition;
            transform.rotation = Quaternion.Euler(12, 0, 0);
        }

    }
    
}
