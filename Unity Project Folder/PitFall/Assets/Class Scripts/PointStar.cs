//This class is used to control the points on the stars in the game. Found it on Udemy

using UnityEngine;

public class PointStar : MonoBehaviour, IPlayerRespawnListener {
	public GameObject Effect;
	public int PointToAdd = 10;
	public AudioClip HitStarSound;

	public void OnTriggerEnter2D(Collider2D other)//Controls the Sound, Effect and if the character collected the star
    {
		if (other.GetComponent<Player> () == null)
			return;

		if (HitStarSound != null)
			AudioSource.PlayClipAtPoint (HitStarSound, transform.position);

		GameManager.Instance.AddPoints (PointToAdd);
		Instantiate (Effect, transform.position, transform.rotation);

		gameObject.SetActive (false);

		FloatingText.Show (string.Format ("+{0}!", PointToAdd), "PointStarText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1.5f, 50));
	}

	public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)//Works with the Respawner to let the game know if the player collected the stars before a checkpoint.
    {
		gameObject.SetActive (true);
	}
}
