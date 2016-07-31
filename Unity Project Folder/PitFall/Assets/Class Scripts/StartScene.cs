//Used to control the start scene of the game. Got it from Udemy and modified it a bit. I also used the SeneManager.LoadScene().

using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour {
	public string LoadLevel;

	public void Update () {
		if (!Input.GetKey (KeyCode.Space)) //modified it from a click of a mouse to a spacebar
			return;

		GameManager.Instance.Reset ();
		SceneManager.LoadScene (LoadLevel);//Added the SceneManager.LoadScene ()
	}
}
