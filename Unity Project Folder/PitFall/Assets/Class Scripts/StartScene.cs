using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {
	public string LoadLevel;

	public void Update () {
		if (!Input.GetKey (KeyCode.Space))
			return;

		GameManager.Instance.Reset ();
		Application.LoadLevel (LoadLevel);

	}
}
