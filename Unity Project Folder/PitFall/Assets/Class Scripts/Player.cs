/*This class is the main class for the player it controls everything that is needed on the player for example:
 * the speed of the player,the height the player can jump, the gravity on the player, if the player is standing on a platform, 
 * what happens to the player at a checkpoint and when the player is killed. Got this from Udemy. I also added a few of my own
 * code with this class.*/

using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance() { return _instance; }

    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;

	public int playerLives;
	public float MaxSpeed = 8;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;
	public int MaxHealth = 100;
	public GameObject OuchEffect;
	public Animator Animator;
	public AudioClip PlayerHitSound;
	public AudioClip PlayerDeathSound;

    public int lives { get; private set; }
    public int Health { get; private set; }
	public bool IsDead { get; private set; }

    private Player()
    {
    }

    public void Awake()
    {
        if (Instance() != null && Instance() != this)
            Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(gameObject);
         
		_controller = GetComponent<CharacterController2D>();
        _isFacingRight = transform.right.x > 0;
		Health = MaxHealth;
        lives = playerLives; //added this to the class to set the lives for the character
    }

    public void Update ()//Controls the dead function of the player
    {
		if (!IsDead)
			HandleInput ();
		
        var movemontFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;

        if (IsDead)
        {
            _controller.SetHorizontalForce(0);
            Health = 0; //added this to the class to set the health to 0
        }
        else
        {
            _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movemontFactor));

            Animator.SetBool("IsGrounded", _controller.State.IsGrounded);
            Animator.SetBool("IsDead", IsDead);
            Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
        }
    }

	public void FinishLevel()//Used with the finish class to tell the game to 
    {
		enabled = false;
		_controller.enabled = false;
        Destroy(Animator); //added this to the class to stop the animation of the character
    }

	public void Kill ()//controls the kill function if the player dies
    {
		_controller.HandleCollisions = false;
		GetComponent<Collider2D>().enabled = false;
		IsDead = true;
		Health = 0;

		_controller.SetForce (new Vector2 (0, 20));

		if (PlayerDeathSound != null)
			AudioSource.PlayClipAtPoint (PlayerDeathSound, transform.position);
	}

	public void RespawnAt (Transform spawnPoint)//controls the respawning of the character
    {
		if (!_isFacingRight)
			Flip ();

		GetComponent<Collider2D>().enabled = true;
		_controller.HandleCollisions = true;
		Health = MaxHealth;

		transform.position = spawnPoint.position;
        IsDead = false;
    }

	public void TakeDamage (int damage)//controls the damage of the players character
    {
		if (PlayerHitSound != null)
			AudioSource.PlayClipAtPoint (PlayerHitSound, transform.position);
		
		FloatingText.Show (string.Format ("-{0}", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner (Camera.main, transform.position, 2f, 60f));

		Instantiate (OuchEffect, transform.position, transform.rotation);
		Health -= damage;

        //Modified this for the lives and game over manager
        if (Health <= 0) {
            SubtractLife();
            if (lives < 0)
            {
                return;
			} else {
				LevelManager.Instance.KillPlayer();
				ResetHealth();
			}      
         }
	}

    private void HandleInput () //controls the key strokes of the players character moving left, right and jumping
    {
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

    private void Flip()//lets the character flip if it is needed
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.right.x < 0;

        if (!_isFacingRight)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0); //added this to the class to flip the animation of the character
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0); //added this to the class to flip the animation of the character
        }
     
        //Debug.LogFormat(string.Format ("Flipped"));
    }

    public void SubtractLife()//Subtracts the lives on the character (This is my own code)
    {
        lives = lives - 1;
    }

    public int GetPlayerLives()//Works with the lives of the player (This is my own code)
    {
        return lives;
    }
    public void ResetHealth()//Resets the health (This is my own code)
    {
        Health = MaxHealth;
    }
}
