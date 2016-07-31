/* This class is used to control the camera and to link it to the players character. This i found on a search on the internet but
*modified at the fixed update function so that the camera stops following the player after death*/

using UnityEngine;

public class CameraController : MonoBehaviour {
	private Vector2 velocity;

	public float smoothTimeY;
	public float smoothTimeX;

	public GameObject player;

	public void start ()
    {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void FixedUpdate () //This is a regular update on the class to keep the camera in the correct position
    {
        //This is to let the function know that if the player dies then the camera must respawn by the players current position
        if (!player.GetComponent<Player>().IsDead) {
            float positionX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
            float positionY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
            transform.position = new Vector3(positionX, positionY, transform.position.z);
        }
	}
}
