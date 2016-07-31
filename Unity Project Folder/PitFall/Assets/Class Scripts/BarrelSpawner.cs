/*I used this class for the barrels spawning, what it does is it respawns the barrels at its starting point.
* I got the idea from Udemy but modified it a bit*/
using UnityEngine;


public class BarrelSpawner : MonoBehaviour {
	public Transform EndDestination;
	public BarrelPath Barrels;

	public float Speed;
	public float BarrelRatePerSecond;

	private float _nextBarrelInSeconds;

	public void Start ()
    {
		_nextBarrelInSeconds = BarrelRatePerSecond;
	}

	public void Update ()//The update function updates in the game every time this class is called
    {
        //This is use to respawn a barrel at every x per second.
        if ((_nextBarrelInSeconds -= Time.deltaTime) > 0)
			return;

		_nextBarrelInSeconds = BarrelRatePerSecond;
		var barrels = (BarrelPath)Instantiate (Barrels, transform.position, transform.rotation);
		barrels.Initalize (EndDestination, Speed);

	}		
}

