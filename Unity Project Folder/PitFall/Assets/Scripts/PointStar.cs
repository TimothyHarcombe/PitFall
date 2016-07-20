﻿using UnityEngine;
using System.Collections;

public class PointStar : MonoBehaviour, IPlayerRespawnListener {
	public GameObject Effect;
	public int PointToAdd = 10;

	public void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player> () == null)
			return;

		GameManager.Instance.AddPoints (PointToAdd);
		Instantiate (Effect, transform.position, transform.rotation);

		gameObject.SetActive (false);
	}

	public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player){
		gameObject.SetActive (true);
	}
}