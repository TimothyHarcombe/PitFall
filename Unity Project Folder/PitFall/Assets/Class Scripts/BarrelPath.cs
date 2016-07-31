/*I used this class for the rolling barrels, what it does is destroys the barrels at its end destination
*of the barrels. I got the idea from Udemy but modified it a bit*/

using UnityEngine;

public class BarrelPath : MonoBehaviour {

	private Transform _endDestination;
	private float _speed;

	public AudioClip DestroySound;
    public GameObject DestroyEffect;
	public void Initalize (Transform endDestination, float speed) //This function is used to initalize the end destination and speed variable for the class. 
    {
		_endDestination = endDestination;
		_speed = speed;
	}

	public void Update () //The update function updates in the game every time this class is called
    {
		transform.position = Vector3.MoveTowards (transform.position, _endDestination.position, Time.deltaTime * _speed);

		var distanceSquared = (_endDestination.transform.position - transform.position).sqrMagnitude;
		if (distanceSquared > .01f * .01f)
			return;
	}

    private void OnTriggerEnter2D(Collider2D other) //This function is used to trigger the collider that controls the destroy barrel effect, sound and barrels at the end destination.
    {
        if (other.gameObject.GetComponent<EndDestination>()) {
            
            //Play the destroy barrel effect
            if (DestroyEffect != null)
                Instantiate(DestroyEffect, transform.position, transform.rotation);
            //Play the destroy barrel sound
            if (DestroySound != null)
                AudioSource.PlayClipAtPoint(DestroySound, transform.position);

            Destroy(gameObject);
        }
    }
}