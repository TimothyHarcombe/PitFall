// This class is used with the CharacterController2D class for the gravity and how fast the player will slide on a slope

using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class ControllerParameters2D
{
	public enum JumpBehavior
	{
		CanJumpOnGround,
		CanJumpAnywhere,
		CantJump
	}

	public Vector2 MaxVelocity = new Vector2(float.MaxValue, float.MaxValue);

	[Range(0, 90)]
	public float SlopeLimit = 30;

	public float Gravity = -25f;

	public JumpBehavior JumpRestrictions;

	public float JumpFrequency = .25f;

	public float JumpMagnitude = 12;
}