using UnityEngine;

public class BarrelAI : MonoBehaviour, IPlayerRespawnListener {
	public float Speed;
	public GameObject DestroyedEffect;

	private CharacterController2D _controller;
	private Vector2 _direction;
	private Vector2 _startPosition;

	public void Start () 
	{
		_controller = GetComponent<CharacterController2D> ();
		_direction = new Vector2 (-1, 0);
		_startPosition = transform.position;
	}

	public void Update () 
	{
		_controller.SetHorizontalForce (_direction.x * Speed);
		if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight)) 
		{
			_direction = -_direction;
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}

	public void OnPlayerRespawnInThisCheckpoint (Checkpoint checkpoint, Player player)
	{
		_direction = new Vector2 (-1, 0);
		transform.localScale = new Vector3 (1, 1, 1);
		transform.position = _startPosition;
		gameObject.SetActive (true);
	}
}
