/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About BasicMovementHandler
* -> Handles basic movement like walking, jogging, sprint
*/

public class BasicMovementHandler : MonoBehaviour
{
    private AnimatorHandler animHandler;
    private InputHandler inputHandler;
    private PlayerMovementBase pMoveBase;
    [HideInInspector] public float ObstacleAheadTime = 0f;
    private float z = 0f, radius = 0f;
    
    public void Init()
    {
        animHandler = GetComponent<AnimatorHandler>();
        inputHandler = animHandler.inputHandler;
        pMoveBase = animHandler.pMoveBase;
    }

    public void Tick()
    {
        SetColliderRadius();

        if (pMoveBase.states.curState >= 0 && pMoveBase.states.curState <= 3)
        {
            pMoveBase.rBody.useGravity = true;
			pMoveBase.coll.isTrigger = false;
        }
    }

    public bool ObstacleAhead()
    {
        Vector3 origin = transform.position;
		Vector3 direction = pMoveBase.states.facingDir * transform.forward;
        float errorDistance = 0.01f;
		float distance = errorDistance;

        switch(pMoveBase.states.curState)
        {
            case(0):
                distance += 0.6f;
                break;
            case(1):
                distance += 0.6f;
                break;
            case(2):
                distance += 0.6f;
                break;
            case(3):
                distance += 0.8f;
                break;
        }

        if (Physics.Raycast(origin, direction, distance, ControllerStatics.obstacle))
            ObstacleAheadTime += Time.deltaTime;
        else
            ObstacleAheadTime = 0f;

        return Physics.Raycast(origin, direction, distance, ControllerStatics.obstacle) && pMoveBase.GetObstacleType() != -1;
    }

    private void SetColliderRadius()
    {
        switch(pMoveBase.states.curState)
		{
			case (0):
				radius = 0.3f;
                z = 0;
				break;
			case (1):
				radius = 0.3f;
                z = 0;
				break;
			case (2):
                z = 0;
				radius = 0.4f;
				break;
			case (3):
                if (ObstacleAheadTime > 0.5f)
                {
                    radius = 0.6f;
                    z = 0;
                }
                else
                {
                    z = pMoveBase.states.facingDir * 0.1f;
				    radius = 0.0f;
                }
				break;
		}
        if (!(pMoveBase.states.currentState == StateHandler.CurrentState.Sliding || pMoveBase.states.currentState == StateHandler.CurrentState.Sliding))
        {
            pMoveBase.coll.radius = radius;
            pMoveBase.coll.center = new Vector3(0, 1, z);
        }
    }
}
