//This class is used with the floating text. Got it from Udemy

using UnityEngine;

public interface IFloatingTextPositioner  {
	bool GetPosition (ref Vector2 position, GUIContent content, Vector2 size);
}
