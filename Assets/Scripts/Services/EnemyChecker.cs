using UnityEngine;

// It is responsible for decrementing the enemiesAlive counter and checking if there are any enemies left in the game.
// And notifying the GameManager when the game is won.
public class EnemyChecker : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] RedEnemySpawner redSpawner;

    string[] enemyTags = { "red enemy", "blue enemy", "RedSwordman", "BlueSwordman" };
    public static int enemiesAlive = 0;

    /* This method decrements the enemiesAlive counter 
     * and checks if the current wave index is equal to the total number of waves 
     * and if there is no enemies left. If both conditions are met, it initiates YouWin() method in Game Manager.
     */
    public void CheckForRemainingEnemies()
    {
        enemiesAlive--;
        Debug.Log("enemies number is " + enemiesAlive);
        if (redSpawner.RedWaveIndex == redSpawner.waves.Length && enemiesAlive <= 0)
        {
            gameManager.YouWin();
        }
    }
}
