//This class is used for the jumping platform in the game. Got it from Udemy

using UnityEngine;

public class JumpPlatform : MonoBehaviour {
	public float JumpMagnitude = 20;
	public AudioClip JumpPadSound;

	public void ControllerEnter2D(CharacterController2D controller)
    {
		if (JumpPadSound != null)
			AudioSource.PlayClipAtPoint (JumpPadSound, transform.position);

		controller.SetVerticalForce (JumpMagnitude);
	}
}
