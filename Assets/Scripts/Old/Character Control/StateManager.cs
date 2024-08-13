/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About StateManager
* -> Contains all the variables for the character controller
*/

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class StateManager : MonoBehaviour
{
    [Header("References")]
	public Transform modelPlaceholder;
	public GameObject modelInit;
    public GameObject modelRootBone;
	[Space]
	public Animator anim;
	public CapsuleCollider coll;
	public Rigidbody rBody;
	public CharacterStates charStates;
	public UserInput uInput;
	[Space]
	[HideInInspector] public Transform mainCam;
	[Header("Variables")]
    public Vector2 AxisDir = Vector2.zero;
    public int facingDir;
    public bool inputActive;
	public bool suddenChange;
	public bool sprint;
	public bool jump;
	public bool slide;
	public bool crouch;
	[Space]
	[SerializeField] private int _curState;
	[SerializeField] private bool _onGround;

	[Header("Constants")]
	public float jumpForce = 8f;
	public float groundDistance = 0.634f;
	public float longVaultDistance = 5.17f;
	public float longMediumVaultDistance = 4.63f;
	public float mediumLongVaultDistance = 4.02f;
	public float mediumVaultDistance = 3.12f;
	public float shortVaultDistance = 2.52f;
	public float veryShortVaultDistance = 1.93f;
	public float nearestVaultDistance = 0f;
	public float jogVaultDistance = 1f;
	public float walkVaultDistance = 0.5f;
	public float idleVaultDistance = 0.3f;
	public float sprintVaultSpeed;
	public float jogVaultSpeed;
	public float walkVaultSpeed;
	public float idleVaultSpeed;
	public float inputEnterRoom;
	public float animTriggerOffset;
	public float obsLowShortMinHeight;
	public float obsLowShortMaxHeight;
	public float obsSlideUnderMinHeight;
	public LayerMask ground;
	public LayerMask obstacles;
	public LayerMask groundAndObs;

	//Private
	private GameObject activeModel;
    public void Init()
	{
		InitModel();
		SetupComponents();
	}

	public void Tick()
	{
		UpdateStates();
		UpdateCharStateNames();
	}

	private void InitModel()
	{
		activeModel = Instantiate(modelInit) as GameObject;
		activeModel.transform.parent = modelPlaceholder;
		activeModel.transform.localEulerAngles = Vector3.zero;
		activeModel.transform.localPosition = Vector3.zero;
		activeModel.transform.localScale = Vector3.one;
        modelRootBone = modelPlaceholder.GetChild(0).GetChild(2).gameObject;
	}

	private void SetupComponents()
	{
		Animator modelAnim = activeModel.GetComponent<Animator>();
		anim.avatar = modelAnim.avatar;
		anim.applyRootMotion = true;
		anim.updateMode = AnimatorUpdateMode.AnimatePhysics;
		anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
		Destroy(modelAnim);

		//rBody.useGravity = false;
		rBody.isKinematic = false;
	}

	private void UpdateCharStateNames()
	{
		_curState = charStates.curState;
		_onGround = charStates.onGround;
	}

    private void UpdateStates()
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.Idle) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.IdleMirror))
			charStates.curState = 0; //Idle
		else if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.Walk) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.WalkBwdLeft) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.WalkBwdRight) | anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.WalkMirror) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.WalkTurn) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.WalkTurnMirror))
			charStates.curState = 1; //Walk
		else if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.Jog) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.JogMirror) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.JogTurn) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.JogTurnMirror) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.StopJog) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.StopJogMirror) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.StartJog) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.StartJogMirror))
			charStates.curState = 2; //Jog
		else if (anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.Sprint) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.SprintMirror) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.SprintTurn) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.SprintTurnMirror) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.StopSprint) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.StopSprintMirror) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.StartSprint) || anim.GetCurrentAnimatorStateInfo(0).IsName(AnimVars.StartSprintMirror))
			charStates.curState = 3; //Sprint
		else
			charStates.curState = 4; //On-hold (i.e when you can't do anything else, eg. Vault, Jump, Airborne)
	}

	private bool RequirementsCleared()
	{
		if (anim.isHuman)
			return true;
		return false;
	}
}
