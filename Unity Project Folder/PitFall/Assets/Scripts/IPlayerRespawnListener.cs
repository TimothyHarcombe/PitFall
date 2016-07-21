/*This class is used with the checkpoint and player class to indicate what happens to the points 
 * and connect it to the players last position in the game when killed*/  

public interface IPlayerRespawnListener {
	void OnPlayerRespawnInThisCheckpoint (Checkpoint checkpoint, Player player);
}
