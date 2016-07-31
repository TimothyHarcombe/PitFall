//This class is used for the checkpoints in the game. I got this from Udemy

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {
	private List<IPlayerRespawnListener> _listeners;

	public void Awake ()
    {
		_listeners = new List<IPlayerRespawnListener> ();
	}

	public void PlayerHitCheckpoint ()//Lets the character know when he hit a checkpoint
    {
		StartCoroutine (PlayerHitCheckpointCo (LevelManager.Instance.CurrentTimeBonus));
	}

	private IEnumerator PlayerHitCheckpointCo (int bonus)//This works with the PlayerHitCheckpoint
    {
		FloatingText.Show ("Chechpoint!", "CheckpointText", new CenteredTextPositioner (.5f));
		yield return new WaitForSeconds (.5f);
		FloatingText.Show (string.Format ("+{0} time bonus!", bonus), "CheckpointText", new CenteredTextPositioner (.5f));
	}

	public void PlayerLeftCheckpoint ()
    {
	}

	public void SpawnPlayer (Player player)//Lets the Player and OnPlayerRespawnInThisCheckpoint know which checkpoit the character hit last 
    {
		player.RespawnAt (transform);

		foreach (var listener in _listeners)
			listener.OnPlayerRespawnInThisCheckpoint (this, player);
	}

	public void AssignObjectToCheckpoint (IPlayerRespawnListener listener)//Assigns the player to the latest checkpoint that was hit
    {
		_listeners.Add (listener);
	}
}
