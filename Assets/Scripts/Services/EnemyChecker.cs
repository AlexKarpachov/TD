using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    private string[] enemyTags = { "red enemy", "blue enemy", "RedSwordman", "BlueSwordman" };

    public void CheckForRemainingEnemies()
    {
        StartCoroutine(CheckForRemainingEnemiesCoroutine());
    }
    IEnumerator CheckForRemainingEnemiesCoroutine()
    {
        yield return null;

        bool enemiesRemaining = false;

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
        }
    }
}
