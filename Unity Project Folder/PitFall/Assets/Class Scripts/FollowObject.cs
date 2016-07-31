//This class is used to follow objects for example its been used for the health bar to follow the player in the game. Found it on Udemy

using UnityEngine;

public class FollowObject : MonoBehaviour {
	public Vector2 Offset;
	public Transform Following;

	public void Update () {
			transform.position = Following.transform.position + (Vector3)Offset;
	}
}
