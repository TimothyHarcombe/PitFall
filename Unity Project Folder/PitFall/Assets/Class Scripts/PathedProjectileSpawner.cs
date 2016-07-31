/*This class is used to control the spawning of the barrels the leave the cannon and the end destination. It is also used for
* the sound, spawn effect, fire rate and the speed of the projectiles. Got it from Udemy.*/

using UnityEngine;

public class PathedProjectileSpawner : MonoBehaviour {
	public Transform Destination;
	public PathedProjectile Projectile;

	public GameObject SpawnEffect;
	public float Speed;
	public float FireRate;
	public Animator Animator;
	public AudioClip SpawnProjectileSound;

	private float _nextShotInSeconds;

	public void Start ()
    {
		_nextShotInSeconds = FireRate;
	}

	public void Update ()//Used to update the info of what the projectiles need to do in the game
    {
		if ((_nextShotInSeconds -= Time.deltaTime) > 0)
			return;

		_nextShotInSeconds = FireRate;
		var projectile = (PathedProjectile)Instantiate (Projectile, transform.position, transform.rotation);
		projectile.Initalize (Destination, Speed);

		if (SpawnEffect != null)
			Instantiate (SpawnEffect, transform.position, transform.rotation);

		if (SpawnProjectileSound != null)
			AudioSource.PlayClipAtPoint (SpawnProjectileSound, transform.position);

	}

	public void OnDrawGizmos () //Used to draw a line for the projectile so it is easier to see in the Editor
    {
		if (Destination == null)
			return;

		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, Destination.position);
	}
}
