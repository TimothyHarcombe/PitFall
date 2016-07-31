//This class is used to control the players instant kill method. Got it from Udemy

using UnityEngine;

public class InstanceKill : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D other)
    {
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
