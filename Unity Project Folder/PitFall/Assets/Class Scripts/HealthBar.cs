//This class is used the give the player a health bar to show how much health the player has in the game.Found it on Udemy

using UnityEngine;

public class HealthBar : MonoBehaviour {
	public Player Player;
	public Transform ForegroundSprite;
	public SpriteRenderer ForegroundRenderer;
	public Color MaxHealthColor = new Color (255 / 255f, 63 / 255f, 63 / 255f);
	public Color MinHealthColor = new Color (64 / 255f, 137 / 255f, 255 / 255f);

	public void Update ()
    {
		var healthPercent = Player.Health / (float)Player.MaxHealth;

		ForegroundSprite.localScale = new Vector3 (healthPercent, 1, 1);
		ForegroundRenderer.color = Color.Lerp (MaxHealthColor, MinHealthColor, healthPercent);
	}
}
