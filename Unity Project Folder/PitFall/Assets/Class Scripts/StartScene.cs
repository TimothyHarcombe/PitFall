using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {
	public string LoadLevel;

	public void Update () {
		if (!Input.GetMouseButtonDown (0))
			return;

		GameManager.Instance.Reset ();
		Application.LoadLevel (LoadLevel);
	}
}
