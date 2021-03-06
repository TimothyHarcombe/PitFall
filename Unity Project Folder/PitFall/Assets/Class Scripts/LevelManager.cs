﻿/*This is the level manager class that controls what happens in the levels of the game. Found it on Udemy site they used the 
 * Application.LoadLevel() but I changed it to the SceneManager.LoadScene().*/

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour {
	public static LevelManager Instance { get; private set; }

	public Player Player{ get; private set; }
	public CameraController Camera { get; private set; }
	public TimeSpan RunningTime { get {return DateTime.UtcNow - _started;}}
	public int CurrentTimeBonus { 
		get 
		{
			var secondDifference = (int) (BonusCutoffSeconds - RunningTime.TotalSeconds);
			return Mathf.Max(0, secondDifference) * BonusSecondMultiplier;
		}
	}

	private List<Checkpoint> _checkpoints;
	private int _currentCheckpointIndex;
	private DateTime _started;
	private int _savedPoints;

	public Checkpoint DebugSpawn;
	public int BonusCutoffSeconds;
	public int BonusSecondMultiplier;

	public void Start ()//Used this function to indicate where in the level is the character, the camera following the character and if the player dies.
    {
		_checkpoints = FindObjectsOfType<Checkpoint> ().OrderBy (t => t.transform.position.x).ToList ();
        _currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

		Player = FindObjectOfType<Player> ();
		Camera = FindObjectOfType<CameraController> ();

		_started = DateTime.UtcNow;

		var listeners = FindObjectsOfType<MonoBehaviour> ().OfType<IPlayerRespawnListener> ();
		foreach (var listener in listeners) {
			for (var i = _checkpoints.Count - 1; i >= 0; i--) {
				var distance = ((MonoBehaviour)listener).transform.position.x - _checkpoints [i].transform.position.x;
				if (distance < 0)
					continue;

				_checkpoints [i].AssignObjectToCheckpoint (listener);
				break;
			}
		}

		#if UNITY_EDITOR
		if (DebugSpawn != null)
			DebugSpawn.SpawnPlayer (Player);
		else if (_currentCheckpointIndex != -1)
			_checkpoints [_currentCheckpointIndex].SpawnPlayer (Player);
		#else
		if (_currentCheckpointIndex != -1)
		_checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
		#endif
	}

    public void Awake()
    {
        _savedPoints = GameManager.Instance.Points;
        Instance = this;
    }


    public void Update () //keeps track on the checkpoints, the distanse of the last checkpoint hit and the points 
    {
		var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;
		if (isAtLastCheckpoint)
			return;

		var distanceToNextCheckpoint = _checkpoints [_currentCheckpointIndex + 1].transform.position.x - Player.transform.position.x;
		if (distanceToNextCheckpoint >= 0)
			return;

		_checkpoints[_currentCheckpointIndex].PlayerLeftCheckpoint();
		_currentCheckpointIndex++;
		_checkpoints[_currentCheckpointIndex].PlayerHitCheckpoint();

		GameManager.Instance.AddPoints (CurrentTimeBonus);
		_savedPoints = GameManager.Instance.Points;
		_started = DateTime.UtcNow;

	}

	public void GotoNextLevel(string levelName) //Used to go to the next level
    {
		StartCoroutine (GotoNextLevelCo (levelName));	
	}

	private IEnumerator GotoNextLevelCo(string levelName) //Used with the GotoNextLevel
    {
		Player.FinishLevel ();
		GameManager.Instance.AddPoints (CurrentTimeBonus);
		FloatingText.Show ("Level Complete!", "CheckpointText", new CenteredTextPositioner (.2f));
		yield return new WaitForSeconds (1f);

		FloatingText.Show (string.Format ("{0} points!", GameManager.Instance.Points), "CheckpointText", new CenteredTextPositioner (.25f));
		yield return new WaitForSeconds (5f);

		if (string.IsNullOrEmpty (levelName))
			SceneManager.LoadScene ("StartScene");
		else
			SceneManager.LoadScene (levelName);
	}

	public void KillPlayer () //Used for when the character falls into the pits or dies 
    {
		StartCoroutine (KillPlayerCo ());
	}

	private IEnumerator KillPlayerCo () //Used with the KillPlayer function
    {
		Player.Kill ();
		yield return new WaitForSeconds(1.4f);

		if (_currentCheckpointIndex != -1)
			_checkpoints [_currentCheckpointIndex].SpawnPlayer (Player);
		_started = DateTime.UtcNow;
		GameManager.Instance.ResetPoints (_savedPoints);		
	}
}
