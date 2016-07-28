using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BarrelPath : MonoBehaviour {

	private Transform _enddestination;
	private float _speed;
	public AudioClip DestroySound;

	public GameObject DestroyEffect;
	public void Initalize (Transform enddestination, float speed) {
		_enddestination = enddestination;
		_speed = speed;
	}

	public void Update () {
		transform.position = Vector3.MoveTowards (transform.position, _enddestination.position, Time.deltaTime * _speed);

		var distanceSquared = (_enddestination.transform.position - transform.position).sqrMagnitude;
		if (distanceSquared > .01f * .01f)
			return;

		if (DestroyEffect != null)
			Instantiate (DestroyEffect, transform.position, transform.rotation);

		Destroy (gameObject);

		if (DestroySound != null)
			AudioSource.PlayClipAtPoint (DestroySound, transform.position);
	}
}