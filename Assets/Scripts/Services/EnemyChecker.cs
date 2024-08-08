using System.Collections;
using System.Linq;
using UnityEngine;

// It is responsible for decrementing the enemiesAlive counter and checking if there are any enemies left in the game.
// And notifying the GameManager when the game is won.
public class EnemyChecker : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] RedEnemySpawner redSpawner;

    // Static variable to keep track of the number of enemies alive.
    public static int enemiesAlive = 0;
    string[] enemyTags = { "red enemy", "blue enemy", "RedSwordman", "BlueSwordman" };

    /* This method decrements the enemiesAlive counter 
     * and checks if the current wave index is equal to the total number of waves 
     * and if there is only one enemy left. If both conditions are met, it starts the CheckForRemainingEnemiesCoroutine() coroutine.
     * */
    public void CheckForRemainingEnemies()
    {
        enemiesAlive--;

        if (redSpawner.RedWaveIndex == redSpawner.waves.Length && enemiesAlive <= 0)
        {
            StartCoroutine(CheckForRemainingEnemiesCoroutine());
        }
    }

    // checks for remaining enemies
    // If no enemies are found, calls the YouWin method on the GameManager script
    IEnumerator CheckForRemainingEnemiesCoroutine()
    {
        yield return null;
        bool enemiesRemaining = false;

        while (true)
        {
            foreach (string tag in enemyTags)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
                enemies = enemies.Where(e => e.activeInHierarchy).ToArray();
                if (enemies.Length > 0)
                {
                    enemiesRemaining = true;
                    break;
                }
            }

            if (!enemiesRemaining)
            {
                gameManager.YouWin();
                yield break;
            }
            yield return null;
        }
        
    }
}
