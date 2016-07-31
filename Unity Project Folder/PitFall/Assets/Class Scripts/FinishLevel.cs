//This class is used to tell the player that they have finished the level. Found it in Udemy
using UnityEngine;

public class FinishLevel : MonoBehaviour {
	public string LevelName;

	public void OnTriggerEnter2D(Collider2D other)//Lets the player know when the character hit the finish trigger object
    {
		if (other.GetComponent<Player> () == null)
			return;

		LevelManager.Instance.GotoNextLevel (LevelName);
	}
}
