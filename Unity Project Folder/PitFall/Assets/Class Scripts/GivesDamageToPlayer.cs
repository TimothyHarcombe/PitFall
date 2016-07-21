//This class it use to give the player damage in the game and to react to the damage given for example get hit backwards

using UnityEngine;
using System.Collections;

public class GivesDamageToPlayer : MonoBehaviour {
	public int DamageToGive = 10;

	private Vector2 _lastPosition, _velocity;

	public void LastUpdate () {
		_velocity = (_lastPosition - (Vector2)transform.position) / Time.deltaTime;
		_lastPosition = transform.position;
	}

	public void OnTriggerEnter2D (Collider2D other) {
		var player = other.GetComponent<Player> ();
		if (player == null)
			return;

		player.TakeDamage (DamageToGive);
		var controller = player.GetComponent<CharacterController2D> ();
		var totalVelocity = controller.Velocity + _velocity;

		controller.SetForce (new Vector2 (
			-1 * Mathf.Sign (totalVelocity.x) * Mathf.Clamp (Mathf.Abs (totalVelocity.x) * 6, 10, 40), 
			-1 * Mathf.Sign (totalVelocity.y) * Mathf.Clamp (Mathf.Abs (totalVelocity.y) * 6, 5, 30)));
	}
}
