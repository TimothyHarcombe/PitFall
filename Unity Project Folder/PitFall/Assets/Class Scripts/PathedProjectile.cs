﻿/* This class is used to Initalize the path of the projectile and it sets the speed, sound and destry effect in the game.
 * Got it from Udemy.*/

using UnityEngine;

public class PathedProjectile : MonoBehaviour {
	private Transform _destination;
	private float _speed;
	public AudioClip DestroySound;

	public GameObject DestroyEffect;
	public void Initalize (Transform destination, float speed)
    {
		_destination = destination;
		_speed = speed;
	}

	public void Update () //Used for the destination of the projectile, destroy object and destroy effect
    {
		transform.position = Vector3.MoveTowards (transform.position, _destination.position, Time.deltaTime * _speed);

		var distanceSquared = (_destination.transform.position - transform.position).sqrMagnitude;
		if (distanceSquared > .01f * .01f)
			return;

		if (DestroyEffect != null)
			Instantiate (DestroyEffect, transform.position, transform.rotation);

		Destroy (gameObject);

		if (DestroySound != null)
			AudioSource.PlayClipAtPoint (DestroySound, transform.position);
	}
}
