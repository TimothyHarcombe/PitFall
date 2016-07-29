/*using UnityEngine;
using System.Collections;

public class Vine : MonoBehaviour {
	private CharacterController2D thePlayer;

	public void Start (){
		thePlayer = FindObjectOfType<CharacterController2D> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "Player") {
			thePlayer.onVine = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.name == "Player") {
			thePlayer.onVine = false;
		}
} 
}*/