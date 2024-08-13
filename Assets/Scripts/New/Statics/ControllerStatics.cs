/**
*   Copyright (c) 2021 - 3021 Aansutons Inc.
*/

using UnityEngine;

/** About ControllerStatics
* -> All the character controller related constants
*/

public static class ControllerStatics
{
    public static float groundDistance = 0.634f;
    public static LayerMask ground = 1 << LayerMask.NameToLayer("ground");
    public static LayerMask obstacle = 1 << LayerMask.NameToLayer("Obstacles");
    public static LayerMask ragdoll = 1 << LayerMask.NameToLayer("Ragdoll");
    public static LayerMask groundAndObs = ground.value | obstacle.value | ragdoll.value;

    public static float obsLowHeight = 0.81f;
    public static float obsShortLength = 0.5f;
    public static float obsMediumLength = 2.5f;
    public static float obsMedHeight = 1.55f;
    public static float obsHighHeight = 2.5f;

    public static float inputEnterRoom = 2f;
	public static float animTriggerOffset = 1.3f;

    public static float longVaultDistance = 4.3f;
	public static float longMediumVaultDistance = 3.34f;
	public static float mediumVaultDistance = 2f;
	public static float shortVaultDistance = 1.1f;
	public static float veryShortVaultDistance = 0.9f;
	public static float nearestVaultDistance = 0f;
	public static float jogVaultDistance = 1.5f;
	public static float walkVaultDistance = 0.5f;
	public static float idleVaultDistance = 0.3f;
    public static float sprintVaultSpeed = 3f;
	public static float jogVaultSpeed = 1f;
	public static float walkVaultSpeed = 0.5f;
	public static float idleVaultSpeed = 0.3f;
}
