/*This class is used to destroy the partical system used for the player getting damage. This is not my
 * own code I got this from Udemy.com*/ 

using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour {
	private ParticleSystem _particaleSystem;

	public void Start () //The start function is called at the start of the game/class
    {
		_particaleSystem = GetComponent<ParticleSystem> ();
	}

	public void Update () //The update function updates in the game every time this class is called
    {
		if (_particaleSystem.isPlaying)
			return;

		Destroy (gameObject);
	}
}
