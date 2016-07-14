using UnityEngine;
using System.Collections;

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
