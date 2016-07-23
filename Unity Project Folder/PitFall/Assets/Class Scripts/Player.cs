/*This class is the main class for the player it controls everything that is needed on the player for example:
 * the speed of the player,the height the player can jump, the gravity on the player, if the player is standing on a platform, 
 * what happens to the player at a checkpoint and when the player is killed*/

using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

    public float MaxSpeed = 8;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;
	public int MaxHealth = 100;
	public GameObject OuchEffect;
	public Animator Animator;
    public int lives;

	public int Health { get; private set; }
	public bool IsDead { get; private set; }

    public void Awake() {
        _controller = GetComponent<CharacterController2D>();
        _isFacingRight = transform.localScale.x > 0;
		Health = MaxHealth;
    }

    public void Update () {
		if (!IsDead)
			HandleInput();

        var movemontFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;

		if (IsDead)
			_controller.SetHorizontalForce (0);
		else
        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movemontFactor));

		Animator.SetBool ("IsGrounded", _controller.State.IsGrounded);
		Animator.SetBool ("IsDead", IsDead);
		Animator.SetFloat ("Speed", Mathf.Abs (_controller.Velocity.x) / MaxSpeed);        
    }

	public void Kill () {
		_controller.HandleCollisions = false;
		GetComponent<Collider2D>().enabled = false;
		IsDead = true;
		Health = 0;

		_controller.SetForce (new Vector2 (0, 20));
	}

	public void RespawnAt (Transform spawnPoint) {
		if (!_isFacingRight)
			Flip ();

		IsDead = false;
		GetComponent<Collider2D>().enabled = true;
		_controller.HandleCollisions = true;
		Health = MaxHealth;

		transform.position = spawnPoint.position;
	}

	public void TakeDamage (int damage) {
		FloatingText.Show (string.Format ("-{0}", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 2f, 60f));

		Instantiate (OuchEffect, transform.position, transform.rotation);
		Health -= damage;

		if (Health <= 0)
			LevelManager.Instance.KillPlayer ();
	}

    private void HandleInput () {
        if (Input.GetKey(KeyCode.D))
        {
            _normalizedHorizontalSpeed = 1;
            if (!_isFacingRight)
                Flip();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _normalizedHorizontalSpeed = 1;
            if (!_isFacingRight)
                Flip();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _normalizedHorizontalSpeed = -1;
            if (_isFacingRight)
                Flip();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _normalizedHorizontalSpeed = -1;
            if (_isFacingRight)
                Flip();
        }
        else
        {
            _normalizedHorizontalSpeed = 0;
        }
        if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space)) {
            _controller.Jump();
        }
    }

    private void Flip() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;

        if (!_isFacingRight)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            return;
        }
        Debug.LogFormat(string.Format ("Flipped"));
    }

    public void PlayerLives()
    {
        lives = lives - 1;
    }

    public int GetPlayerLives()
    {
        return lives;
    }
}
