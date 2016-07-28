//This class is used to control the players instant kill method

using UnityEngine;
using System.Collections;

public class InstanceKill : MonoBehaviour {

	private LevelManager levelManager;

	public void OnTriggerEnter2D(Collider2D other) {
		var player = other.GetComponent<Player> ();
		if (player == null){
			return;
		} else {
			LevelManager.Instance.KillPlayer();
			player.ResetHealth();
			player.SubtractLife();
		}
	}
}
