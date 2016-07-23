//This class is used to destroy the partical system used for the player getting damage

using UnityEngine;

public class AutoDestroyParticleSystem : MonoBehaviour {
	private ParticleSystem _particaleSystem;

	public void Start () {
		_particaleSystem = GetComponent<ParticleSystem> ();
	}

	public void Update () {
		if (_particaleSystem.isPlaying)
			return;

		Destroy (gameObject);
	}
}
