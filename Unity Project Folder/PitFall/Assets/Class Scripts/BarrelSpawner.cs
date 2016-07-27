/*
 * using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class BarrelSpawner : MonoBehaviour {
	public Transform Destination;
	public Transform Destination2;
	public Transform DestinationEnd;
	public BarrelPath Barrel;

	public GameObject SpawnEffect;
	public float Speed;
	public float BarrelAmountRate;
	public Animator Animator;
	public AudioClip BarrelSound;

	private float _nextBarrelInSeconds;

	public void Start () {
		_nextBarrelInSeconds = BarrelAmountRate;
	}

	public void Update () {
		if ((_nextBarrelInSeconds -= Time.deltaTime) > 0)
			return;

		_nextBarrelInSeconds = BarrelAmountRate;
		var barrel = (BarrelPath)Instantiate (Barrel, transform.position, transform.rotation);
		barrel.Initalize (Destination, Destination2, DestinationEnd, Speed);

		if (SpawnEffect != null)
			Instantiate (SpawnEffect, transform.position, transform.rotation);

		if (BarrelSound != null)
			AudioSource.PlayClipAtPoint (BarrelSound, transform.position);

	}
		
}*/

