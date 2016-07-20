using UnityEngine;

public class CenteredTextPositioner : IFloatingTextPositioner {
	private readonly float _speed;
	private float _textPosition;

	public CenteredTextPositioner(float speed) {
		_speed = speed;
	}

	public bool GetPosition (ref Vector2 position, GUIContent content, Vector2 size) 
	{
		return false;
	}
}
