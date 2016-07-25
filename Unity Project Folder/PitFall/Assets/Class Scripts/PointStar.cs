//This class is used to control the points on the stars in the game

using UnityEngine;

public class PointStar : MonoBehaviour, IPlayerRespawnListener {
	public GameObject Effect;
	public int PointToAdd = 10;
	public AudioClip HitStarSound;

	public void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player> () == null)
			return;

		if (HitStarSound != null)
			AudioSource.PlayClipAtPoint (HitStarSound, transform.position);

		GameManager.Instance.AddPoints (PointToAdd);
		Instantiate (Effect, transform.position, transform.rotation);

		gameObject.SetActive (false);

		FloatingText.Show (string.Format ("+{0}!", PointToAdd), "PointStarText", new FromWorldPointTextPositioner (Camera.main, transform.position, 1.5f, 50));
	}

	public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player){
		gameObject.SetActive (true);
	}
}
