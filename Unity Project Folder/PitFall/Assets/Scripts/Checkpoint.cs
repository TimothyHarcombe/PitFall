﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {
	private List<IPlayerRespawnListener> _listeners;

	public void Awake () {
		_listeners = new List<IPlayerRespawnListener> ();
	}

	public void PlayerHitCheckpoint () {
	}

	private IEnumerator PlayerHitCheckpointCo (int bonus) {
		yield break;
	}

	public void PlayerLeftCheckpoint () {
	}

	public void SpawnPlayer (Player player) {
		player.RespawnAt (transform);

		foreach (var listener in _listeners)
			listener.OnPlayerRespawnInThisCheckpoint (this, player);
	}

	public void AssignObjectToCheckpoint (IPlayerRespawnListener listener) {
		_listeners.Add (listener);
	}
}