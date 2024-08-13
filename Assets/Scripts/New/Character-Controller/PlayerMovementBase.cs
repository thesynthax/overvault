/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About PlayerMovementBase
* -> The base class of all character controller related stuff
*/

public class PlayerMovementBase : MonoBehaviour
{
	[SerializeField] private GameObject modelInit;
	public StateHandler states;
	[HideInInspector] public Animator anim;
	[HideInInspector] public CapsuleCollider coll;
	[HideInInspector] public Rigidbody rBody;
	[HideInInspector] public InputHandler inputHandler;
	[HideInInspector] public Transform mainCam;
	[HideInInspector] public GameObject modelRootBone;
	private GameObject activeModel;
	private Transform modelPlaceholder;

	[HideInInspector] public BasicMovementHandler basicMovement;
	[HideInInspector] public VaultHandler vaultHandler;
	[HideInInspector] public SlideCrouchHandler slideCrouchHandler;
	[HideInInspector] public ClimbHandler climbHandler;
	[HideInInspector] public BasicAirMovementHandler basicAirMovement;
	[HideInInspector] public RagdollControl ragdollControl;
	
	private void InitComponents()
	{
		anim = GetComponent<Animator>();
		coll = GetComponent<CapsuleCollider>();
		rBody = GetComponent<Rigidbody>();
		inputHandler = GetComponent<InputHandler>();

		basicMovement = GetComponent<BasicMovementHandler>();
		vaultHandler = GetComponent<VaultHandler>();
		slideCrouchHandler = GetComponent<SlideCrouchHandler>();
		climbHandler = GetComponent<ClimbHandler>();
		basicAirMovement = GetComponent<BasicAirMovementHandler>();

		ragdollControl = GetComponent<RagdollControl>();
	}
	
	private void InitModel()
	{
		modelPlaceholder = transform.GetChild(1);
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
        mainCam = Camera.main.transform;
	}
    private void Start()
    {
		InitComponents();
        InitModel();
        SetupComponents();

		basicMovement.Init();
		vaultHandler.Init();
		slideCrouchHandler.Init();
		climbHandler.Init();
		basicAirMovement.Init();
    }
	
    private void Update()
    {
		UpdateStates();

        states.onGround = OnGround();
		states.facingDir = FacingDir(modelRootBone.transform.localEulerAngles);

		basicMovement.Tick();
		vaultHandler.Tick();
		slideCrouchHandler.Tick();
		climbHandler.Tick();
		basicAirMovement.Tick();
    }

    private void LateUpdate() 
    {
        vaultHandler.LateTick();
    }

	private void UpdateStates()
	{
		AnimatorStateInfo currentAnim = anim.GetCurrentAnimatorStateInfo(0);
		if (ragdollControl.ragdollState == RagdollControl.RagdollState.animated)
		{
			if (currentAnim.IsTag("idle"))
			{
				states.curState = 0; //Idle
				states.currentState = StateHandler.CurrentState.Idle;
			}
			else if (currentAnim.IsTag("walk"))
			{
				states.curState = 1; //Walk
				states.currentState = StateHandler.CurrentState.Walking;
			}
			else if (currentAnim.IsTag("jog"))
			{
				states.curState = 2; //Jog
				states.currentState = StateHandler.CurrentState.Jogging;
			}
			else if (currentAnim.IsTag("sprint"))
			{
				states.curState = 3; //Sprint
				states.currentState = StateHandler.CurrentState.Sprinting;
			}
			else
			{
				states.curState = 4; //On-hold (i.e when you can't do anything else, eg. Vault, Jump, Airborne)

				if (currentAnim.IsTag("vault"))
				{
					states.currentState = StateHandler.CurrentState.Vaulting;
				}
				else if (currentAnim.IsTag("slide"))
				{
					states.currentState = StateHandler.CurrentState.Sliding;
				}
				else if (currentAnim.IsTag("crouch"))
				{
					states.currentState = StateHandler.CurrentState.Crouching;
				}
				else if (currentAnim.IsTag("climb"))
				{
					states.currentState = StateHandler.CurrentState.Climbing;
				}
				else if (currentAnim.IsTag("jump"))
				{
					states.currentState = StateHandler.CurrentState.Jumping;
				}
				else if (currentAnim.IsTag("fall"))
				{
					states.currentState = StateHandler.CurrentState.Falling;
				}
				else if (currentAnim.IsTag("land"))
				{
					states.currentState = StateHandler.CurrentState.Landing;
				}
				else if (currentAnim.IsTag("roll"))
				{
					states.currentState = StateHandler.CurrentState.Rolling;
				}
				else if (currentAnim.IsTag("ledge"))
				{
					states.currentState = StateHandler.CurrentState.Ledge;
				}
			}
		}
		else 
		{
			states.curState = 4;
			states.currentState = StateHandler.CurrentState.Ragdolled;
		}
		
	}

	enum ObstacleType
	{
		LowShort, //vault
		LowMedium, //idle, walk, jog - step up and run. sprint - vault
		LowLong, //can only step over it, no vaults
		Medium, //climb, but not able to hang on ledge
		High, //climb, able to hang on ledge
	}

	public int GetObstacleType()
	{
		ObstacleType obsType = new ObstacleType();
		Vector3 origin = transform.position;
		Vector3 direction = states.facingDir * transform.forward;
		float distance = ControllerStatics.longVaultDistance + ControllerStatics.inputEnterRoom;
		float errorDistance = 0.01f;
		RaycastHit hit = new RaycastHit();

		if (Physics.Raycast(origin, direction, out hit, distance, ControllerStatics.obstacle))
		{
			if (!Physics.Raycast(origin + transform.up * (ControllerStatics.obsLowHeight + errorDistance), direction, distance, ControllerStatics.obstacle))
			{
				if (!Physics.Raycast(hit.point + transform.up * (ControllerStatics.obsLowHeight + errorDistance) + states.facingDir * transform.forward * (ControllerStatics.obsShortLength + errorDistance), Vector3.down, 2 * errorDistance, ControllerStatics.obstacle))
				{
					obsType = ObstacleType.LowShort;
				}
				else
				{
					if (!Physics.Raycast(hit.point + transform.up * (ControllerStatics.obsLowHeight + errorDistance) + states.facingDir * transform.forward * (ControllerStatics.obsMediumLength + errorDistance), Vector3.down, 2 * errorDistance, ControllerStatics.obstacle))
					{
						obsType = ObstacleType.LowMedium;
					}
					else
					{
						obsType = ObstacleType.LowLong;
					}
				}
			}
			else
			{
				if (!Physics.Raycast(origin + transform.up * (ControllerStatics.obsMedHeight + errorDistance), direction, distance, ControllerStatics.obstacle))
				{
					obsType = ObstacleType.Medium;
				}
				else
				{
					if (!Physics.Raycast(origin + transform.up * (ControllerStatics.obsHighHeight + errorDistance), direction, distance, ControllerStatics.obstacle))
					{
						obsType = ObstacleType.High;
					}
				}
			}
			return (int)obsType;
		}

		return -1;
	}

	private bool OnGround()
	{
		bool r = false;

		Vector3 origin = transform.position + (Vector3.up * 0.55f);

		RaycastHit hit = new RaycastHit();
		bool isHit = false;
		FindGround(origin, ref hit, ref isHit);

		if (!isHit)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector3 newOrigin = origin;

				switch (i)
				{
					case 0:
						newOrigin += Vector3.forward / 3;
						break;
					case 1:
						newOrigin -= Vector3.forward / 3;
						break;
					case 2:
						newOrigin += Vector3.right / 3;
						break;
					case 3:
						newOrigin -= Vector3.right / 3;
						break;
				}

				FindGround(newOrigin, ref hit, ref isHit);

				if (isHit)
				{
					break;
				}
			}
		}

		r = isHit;

		return r;
	}
	
	private void FindGround(Vector3 origin, ref RaycastHit hit, ref bool isHit)
	{
		Debug.DrawRay(origin, -Vector3.up * 0.5f, Color.red);
		if (Physics.Raycast(origin, -Vector3.up, out hit, ControllerStatics.groundDistance, ControllerStatics.groundAndObs))
		{
			isHit = true;
		}
	}

	private int FacingDir(Vector3 rootRotation)
    {
        int facingDir = 0;
        if ((rootRotation.y <= 30 && rootRotation.y >= 0) || (rootRotation.y < 360 && rootRotation.y >= 330))
        {
            facingDir = 1;
        }
        else if (rootRotation.y <= 210 && rootRotation.y >= 150)
        {
            facingDir = -1;
        }
        return facingDir;
    }
}
