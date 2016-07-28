using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class BarrelSpawner : MonoBehaviour {
	public Transform EndDestination;
	public BarrelPath Barrels;

	public float Speed;
	public float BarrelRatePerSecond;

	private float _nextBarrelInSeconds;

	public void Start () {
		_nextBarrelInSeconds = BarrelRatePerSecond;
	}

	public void Update () {
		if ((_nextBarrelInSeconds -= Time.deltaTime) > 0)
			return;

		_nextBarrelInSeconds = BarrelRatePerSecond;
		var barrels = (BarrelPath)Instantiate (Barrels, transform.position, transform.rotation);
		barrels.Initalize (EndDestination, Speed);

	}		
}

