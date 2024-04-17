using System.Collections;
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
    [SerializeField] int waveTimer = 5;
    [SerializeField] int startDelay = 3;

    public bool gameOver = false;

    private void Start()
    {
        // 1st wave countdown
        Invoke ("CoroutineStarter", startDelay);
    }

    void CoroutineStarter()
    {
        StartCoroutine(RedEnemySpawner());
        StartCoroutine(BlueEnemySpawner());
    }

    IEnumerator RedEnemySpawner()
    {
        if (gameOver)
        {
            StopAllCoroutines();
            yield break;
        }
        

        int needSpawn = redWave * redEnemiesInWave;
        
        /*if(needSpawn == 1)
        {
            yield return new WaitForSeconds(3);
        }*/

        //Countdown until next wave

        while (needSpawn > 0 && !gameOver)
        {
            GameObject tempRedEnemy = Instantiate(redEnemy, leftSpawnPoint.position, Quaternion.identity);
            EnemyLogic tempLogic = tempRedEnemy.GetComponent<EnemyLogic>();

            tempLogic.MoveTo(endPoint);
            needSpawn--;
            yield return new WaitForSeconds(2);
        }
        Debug.Log("Time until next wave " + waveTimer);
        yield return new WaitForSeconds(waveTimer);
        redWave++;
        
        StartCoroutine(RedEnemySpawner());
    }

    IEnumerator BlueEnemySpawner()
    {
        if (gameOver)
        {
            StopAllCoroutines();
            yield break;
        }
        int needSpawn = blueWave * blueEnemiesInWave;

        /*if (needSpawn == 1)
        {
            yield return new WaitForSeconds(3);
        }*/

        //Countdown until next wave

        while (needSpawn > 0 && !gameOver)
        {
            GameObject tempBlueEnemy = Instantiate(blueEnemy, rightSpawnPoint.position, Quaternion.identity);
            EnemyLogic tempLogic = tempBlueEnemy.GetComponent<EnemyLogic>();
            tempLogic.MoveTo(endPoint);
            needSpawn--;
            yield return new WaitForSeconds(2);
        }
        yield return new WaitForSeconds(5);
        blueWave++;
        StartCoroutine(BlueEnemySpawner());
    }
}
