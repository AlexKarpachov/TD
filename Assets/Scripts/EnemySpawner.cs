using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int redWave;
    [SerializeField] int redEnemiesInWave;
    [SerializeField] GameObject redEnemy;
    [SerializeField] Transform leftSpawnPoint;

    [SerializeField] int blueWave;
    [SerializeField] int blueEnemiesInWave;
    [SerializeField] GameObject blueEnemy;
    [SerializeField] Transform rightSpawnPoint;

    [SerializeField] Transform endPoint;

    bool gameOver = false;

    private void Start()
    {
        StartCoroutine(RedEnemySpawner());
        StartCoroutine(BlueEnemySpawner());
    }

    IEnumerator RedEnemySpawner()
    {
        int needSpawn = redWave*redEnemiesInWave;

        while (!gameOver && needSpawn>0)
        {
            GameObject tempRedEnemy = Instantiate(redEnemy, leftSpawnPoint.position, Quaternion.identity);
            EnemyLogic tempLogic = tempRedEnemy.GetComponent<EnemyLogic>();

            tempLogic.MoveTo(endPoint);
            needSpawn--;
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(5);
        redWave++;
        StartCoroutine(RedEnemySpawner());
    }

    IEnumerator BlueEnemySpawner()
    {
        int needSpawn = blueWave*blueEnemiesInWave;

        while (needSpawn > 0 && !gameOver)
        {
            GameObject tempBlueEnemy = Instantiate(blueEnemy, rightSpawnPoint.position, Quaternion.identity);
            EnemyLogic tempLogic = tempBlueEnemy.GetComponent<EnemyLogic>();
            tempLogic.MoveTo(endPoint);
            needSpawn-- ;
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(5);
        blueWave++;
        StartCoroutine(BlueEnemySpawner());
    }
}
