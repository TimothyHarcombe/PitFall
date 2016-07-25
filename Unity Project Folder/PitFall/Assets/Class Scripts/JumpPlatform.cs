//This class is used in the game for the platforms the a player can jump on 

using UnityEngine;
using System.Collections;

public class JumpPlatform : MonoBehaviour {
	public float JumpMagnitude = 20;
	public AudioClip JumpPadSound;

	public void ControllerEnter2D(CharacterController2D controller) {
		if (JumpPadSound != null)
			AudioSource.PlayClipAtPoint (JumpPadSound, transform.position);

		controller.SetVerticalForce (JumpMagnitude);
	}
}
