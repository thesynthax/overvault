/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
using System.Collections;

/** About VaultHandler
* -> All the parkour vault related stuff
*/

public class VaultHandler : MonoBehaviour
{
    private AnimatorHandler animHandler;
    private InputHandler inputHandler;
    private PlayerMovementBase pMoveBase;

    private bool vaultActive = false;

    public void Init()
    {
        animHandler = GetComponent<AnimatorHandler>();
        inputHandler = animHandler.inputHandler;
        pMoveBase = animHandler.pMoveBase;
        StartCoroutine(hello());
    }

    IEnumerator hello() {
        for (int i = 0; i < 10; i++) {
            Debug.Log(i);
            yield return null;
        }
    }

    public void Tick()
    {
        if (pMoveBase.states.currentState == StateHandler.CurrentState.Vaulting)
        {
            pMoveBase.rBody.useGravity = false;
            pMoveBase.coll.isTrigger = true;
        }

        //if (Vault() != -1) Debug.Log(Vault());
    }

    public void LateTick() {}



    public int Vault()
    {
        if (inputHandler.JumpButton.Pressed)
        {
            Vector3 origin = transform.position;
			RaycastHit hit = new RaycastHit();
			Vector3 direction = pMoveBase.states.facingDir * transform.forward;

            int vaultType = -1;

            float inputEnterRoom = ControllerStatics.inputEnterRoom;
			float animTriggerOffset = ControllerStatics.animTriggerOffset;

            float t = 0f;

            if (((Physics.Raycast(origin, direction, out hit, ControllerStatics.longVaultDistance + inputEnterRoom, ControllerStatics.obstacle) && pMoveBase.states.curState != 0) || (Physics.Raycast(origin, direction, out hit, inputEnterRoom, ControllerStatics.obstacle) && pMoveBase.states.curState == 0)))
            {
                Vector3 startPos = transform.position;
                Vector3 targetPos = Vector3.zero;
				
				vaultActive = true;

                if (pMoveBase.states.currentState != StateHandler.CurrentState.Vaulting)
                {
                    switch(pMoveBase.GetObstacleType())
                    {
                        case(0):
                            targetPos = hit.point - pMoveBase.states.facingDir * transform.forward * 0.4f;
                            switch(pMoveBase.states.curState)
                            {
                                case(3):
                                    if (hit.distance <= ControllerStatics.longVaultDistance + inputEnterRoom && hit.distance >= ControllerStatics.longVaultDistance + animTriggerOffset)
                                    {
                                        //vaultType = LowVault(ControllerStatics.sprintVaultSpeed, ControllerStatics.longVaultDistance, 5, ref t, hit, startPos, direction);
                                        vaultType = 5;
                                        targetMatching(targetPos, AvatarTarget.LeftHand, 0.6f, 0.72f);
                                    }
                                    else if (hit.distance <= ControllerStatics.longVaultDistance + animTriggerOffset && hit.distance >= ControllerStatics.longMediumVaultDistance + animTriggerOffset)
                                    {
                                        //vaultType = LowVault(ControllerStatics.sprintVaultSpeed, ControllerStatics.longMediumVaultDistance, 4, ref t, hit, startPos, direction);
                                        vaultType = 4;
                                        targetMatching(targetPos, AvatarTarget.LeftHand, 0.57f, 0.68f);
                                    }
                                    else if (hit.distance <= ControllerStatics.longMediumVaultDistance + animTriggerOffset && hit.distance >= ControllerStatics.mediumVaultDistance + animTriggerOffset)
                                    {
                                        //vaultType = LowVault(ControllerStatics.sprintVaultSpeed, ControllerStatics.mediumVaultDistance, 3, ref t, hit, startPos, direction);
                                        vaultType = 3;
                                        targetMatching(targetPos, AvatarTarget.LeftHand, 0.38f, 0.46f);
                                    }
                                    else if (hit.distance <= ControllerStatics.mediumVaultDistance + animTriggerOffset && hit.distance >= ControllerStatics.shortVaultDistance + animTriggerOffset)
                                    {
                                        vaultType = LowVault(ControllerStatics.sprintVaultSpeed, ControllerStatics.shortVaultDistance, 2, ref t, hit, startPos, direction);
                                    }
                                    else if (hit.distance <= ControllerStatics.shortVaultDistance + animTriggerOffset && hit.distance >= ControllerStatics.nearestVaultDistance)
                                    {
                                        vaultType = LowVault(ControllerStatics.sprintVaultSpeed, ControllerStatics.veryShortVaultDistance, 1, ref t, hit, startPos, direction);
                                        //vaultType = 1;
                                        //targetMatching(targetPos, AvatarTarget.LeftHand, 0.33f, 0.45f);
                                    }
                                    break;
                                case(2):
                                    if (hit.distance <= inputEnterRoom)
                                    {
                                        vaultType = LowVault(ControllerStatics.jogVaultSpeed, ControllerStatics.jogVaultDistance, Random.Range(1,4), ref t, hit, startPos, direction);
                                    }
                                    break;
                                case(1):
                                    if (hit.distance <= inputEnterRoom)
                                    {
                                        targetPos = hit.point;// + transform.up * ControllerStatics.obsLowHeight;
                                        //vaultType = LowVault(ControllerStatics.walkVaultSpeed, ControllerStatics.walkVaultDistance, Random.Range(1,3), ref t, hit, startPos, direction);
                                        vaultType = Random.Range(1,3);
                                        if (vaultType == 1)
                                            targetMatching(targetPos, AvatarTarget.LeftHand, 0.29f, 0.65f);
                                        else
                                            targetMatching(targetPos, AvatarTarget.LeftHand, 0.25f, 0.59f);
                                    }
                                    break;
                                case(0):
                                    if (hit.distance <= inputEnterRoom * 0.6f)
                                    {
                                        vaultType = LowVault(ControllerStatics.idleVaultSpeed, ControllerStatics.idleVaultDistance, Random.Range(1,3), ref t, hit, startPos, direction);
                                    }
                                    break;
                            }
                            break;
                        case(1):
                            if (pMoveBase.states.curState == 3)
                            {
                                if (hit.distance <= 3 * inputEnterRoom && hit.distance > 2 * inputEnterRoom)
                                    vaultType = LowVault(ControllerStatics.sprintVaultSpeed, 2f, 5, ref t, hit, startPos, direction);
                                else if (hit.distance <= 2 * inputEnterRoom)
                                    vaultType = LowVault(ControllerStatics.sprintVaultSpeed, 1f, 6, ref t, hit, startPos, direction);
                            }
                            break;
                    }
                }

                return vaultType;
            }
            else
            {
                vaultActive = false;
            }
        }
        

        return -1;
    }

    private int LowVault(float speed, float distance, int vaultType, ref float t, RaycastHit hit, Vector3 startPos, Vector3 direction)
    {
        Vector3 endPos = hit.point - direction.normalized * distance;
        t += Time.deltaTime * ControllerStatics.sprintVaultSpeed;

        if (t > 1)
        {
            vaultActive = false;
        }
        
        Vector3 targetPos = Vector3.Lerp(startPos, endPos, t);
        transform.position = targetPos;
        //transform.position = endPos;

        return vaultType;
    }

    private void targetMatching(Vector3 targetPos, AvatarTarget bodyPart, float startTime, float endTime) {
        MatchTargetWeightMask mask = new MatchTargetWeightMask(new Vector3(1, 1, 1), 0);
        if (vaultActive)
            pMoveBase.anim.MatchTarget(targetPos, transform.rotation, bodyPart, mask, startTime, endTime);
    }
}
