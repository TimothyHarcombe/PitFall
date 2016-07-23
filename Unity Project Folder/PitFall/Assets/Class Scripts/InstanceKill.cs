﻿//This class is used to control the players instant kill method

using UnityEngine;
using System.Collections;

public class InstanceKill : MonoBehaviour {
	public void OnTriggerEnter2D(Collider2D other) {
		var player = other.GetComponent<Player> ();
		if (player == null)
			return;
        if (player.lives < 1)
        {
            // Game over function call
            // I.E LevelManager.Instance.GameOver;
        }
        else
        {
            LevelManager.Instance.KillPlayer();
            player.ResetHealth();
            player.SubtractLife();
        }
	}
}
