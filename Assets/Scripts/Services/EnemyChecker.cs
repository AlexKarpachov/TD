using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    private string[] enemyTags = { "red enemy", "blue enemy", "RedSwordman", "BlueSwordman" };

    GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

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
            Debug.Log(tag + ": " + enemies.Length);
            if (enemies.Length > 0)
            {
                enemiesRemaining = true;
                break;
            }
        }

        if (!enemiesRemaining)
        {
            Debug.Log("All enemies were destroyed");
            gameManager.YouWin();
        }
    }
}
