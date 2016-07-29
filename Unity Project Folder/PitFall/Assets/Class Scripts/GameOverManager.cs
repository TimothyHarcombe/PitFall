using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	private StartScene startScene;
	public Player player;
	public float restartDelay = 5f;

	Animator anim;
	float restartTimer;

	void Awake () {
		anim = GetComponent<Animator> ();	
	}

	void Update () {
		if (player == null)
			return;
		if (player.lives <= 0) {
			anim.SetTrigger ("GameOver");
			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay) {
				SceneManager.LoadScene ("StartScene");
				Destroy (gameObject);
			}
		}
	}
}
