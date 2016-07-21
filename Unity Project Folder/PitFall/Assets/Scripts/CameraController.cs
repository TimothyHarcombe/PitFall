// This class is used to control the camera and to link it to the players character

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	private Vector2 velocity;

	public float smoothTimeY;
	public float smoothTimeX;

	public GameObject player;

	public void start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void FixedUpdate () {
		float positionX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float positionY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (positionX, positionY, transform.position.z);
	}
}
