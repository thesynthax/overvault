/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;
//using UnityEditor.Animations;

/** About AnimatorHandler
* -> Handles all animation related stuff
*/

public class AnimatorHandler : MonoBehaviour
{	
	private bool yRootMotion = true;
    [HideInInspector] public PlayerMovementBase pMoveBase;
    [HideInInspector] public InputHandler inputHandler;
    private Animator anim;
    private Rigidbody rBody;
    private CapsuleCollider coll;

    //[HideInInspector] public AnimatorState animState;
    //[HideInInspector] public AnimatorStateTransition animTransition;

    private void Start()
    {
        pMoveBase = GetComponent<PlayerMovementBase>();
        inputHandler = pMoveBase.inputHandler;
        anim = pMoveBase.anim;
        rBody = pMoveBase.rBody;
        coll = pMoveBase.coll;
    }

    private void Update()
    {
        yRootMotion = pMoveBase.states.onGround;
        Animate(pMoveBase.ragdollControl.gettingup, pMoveBase.ragdollControl.animType, (int)pMoveBase.states.currentState, pMoveBase.slideCrouchHandler.UnderObstacleTime(), pMoveBase.climbHandler.Climb(), pMoveBase.GetObstacleType(), pMoveBase.basicMovement.ObstacleAheadTime, pMoveBase.basicMovement.ObstacleAhead(), pMoveBase.vaultHandler.Vault(), inputHandler.CrouchButton.Pressing, inputHandler.SlideButton.Pressing, inputHandler.JumpButton.Pressing, inputHandler.SprintButton.Pressing, inputHandler.HorizontalJoystick.Pressing || inputHandler.VerticalJoystick.Pressing, inputHandler.HorizontalJoystick.value, inputHandler.VerticalJoystick.value, pMoveBase.states.onGround, pMoveBase.states.facingDir);
    }
    
    public void Animate(bool gettingup, int getup, int curState, float underObstacleTime, int climbType, int obstacleType, float obstacleAheadTime, bool obstacleAhead, int vaultType, bool crouch, bool slide, bool jump, bool sprint, bool inputActive, float horz, float vert, bool onGround, int facingDir)
    {
        anim.SetFloat(AnimatorStatics.Horizontal, horz, 0.01f, Time.deltaTime);
        anim.SetFloat(AnimatorStatics.Vertical, vert, 0.01f, Time.deltaTime);
        anim.SetBool(AnimatorStatics.OnGround, onGround);
        anim.SetInteger(AnimatorStatics.FacingDir, facingDir);
        anim.SetBool(AnimatorStatics.InputActive, inputActive);
		anim.SetBool(AnimatorStatics.sprint, sprint);
		anim.SetBool(AnimatorStatics.Jump, jump || (vaultType > -1));
		anim.SetInteger(AnimatorStatics.VaultType, vaultType);
		anim.SetBool(AnimatorStatics.Slide, slide);
		anim.SetBool(AnimatorStatics.Crouch, crouch);
        anim.SetBool(AnimatorStatics.ObstacleAhead, obstacleAhead);
        anim.SetFloat(AnimatorStatics.ObstacleAheadTime, obstacleAheadTime);
        anim.SetInteger(AnimVars.ObstacleType, obstacleType);	
        anim.SetInteger(AnimatorStatics.ClimbType, climbType);
        anim.SetFloat(AnimatorStatics.UnderObstacleTime, underObstacleTime);
        anim.SetInteger(AnimatorStatics.CurState, curState);
        anim.SetInteger(AnimatorStatics.GetUp, getup);
        
        anim.SetBool("ok", Input.GetKey(KeyCode.J));

    }

    /* private void OnAnimatorMove()
    {
        if (pMoveBase.states.onGround && Time.deltaTime > 0)
		{
			Vector3 v = anim.deltaPosition / Time.deltaTime;

			if(!yRootMotion)
				v.y = rBody.velocity.y;

			rBody.velocity = v;
		}
    } */
    private void OnAnimatorMove()
    {
	    if (Time.deltaTime > 0)
	    {
		    Vector3 v = anim.deltaPosition / Time.deltaTime;
		    
		    //Root motion disabled in y-axis
		    if (pMoveBase.states.onGround && pMoveBase.states.currentState != StateHandler.CurrentState.Climbing && pMoveBase.states.currentState != StateHandler.CurrentState.Jumping)
		    {
			    v.y = rBody.velocity.y;
			    rBody.velocity = v;
		    }
			//Root motion enabled in y-axis
		    if (pMoveBase.states.currentState == StateHandler.CurrentState.Climbing || pMoveBase.states.currentState == StateHandler.CurrentState.Jumping)
		    {
			    rBody.velocity = v;
		    }
	    }
    }
}
