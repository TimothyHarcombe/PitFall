/*This class controls the Game over scene in the game and lets it reset to the start scene when character is dead. 
 * I got this from Unity site and added the using UnityEngine.SceneManagement to it because they still used the 
 * Application.loadlevel() to load the levels and that is the old method in doing the level loader.*/ 

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	public Player player;
	public float restartDelay = 5f;

	Animator anim;
	float restartTimer;

	void Awake () //this will load the animation for the game over scene
    {
		anim = GetComponent<Animator> ();	
	}

	void Update ()//Will let the game know when the player died to load the game over scene
    {
		if (player == null)
			return;
		if (player.lives <= 0) {
			anim.SetTrigger ("GameOver");
			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay) {
				SceneManager.LoadScene ("StartScene"); //I used the SceneManager instead of the Application.loadlevel 
				Destroy (gameObject);
			}
		}
	}
}
