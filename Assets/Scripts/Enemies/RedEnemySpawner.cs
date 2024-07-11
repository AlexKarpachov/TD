using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedEnemySpawner : MonoBehaviour
{
    public WaveRed[] waves;
    [Header("Red enemies info")]
    [SerializeField] Transform redSpawnPoint;
    [SerializeField] int redWaveIndex = 0;
    public int RedWaveIndex { get { return redWaveIndex; } }
    bool isRedWaveSpawning = false;
    public bool IsRedWaveSpawning { get { return isRedWaveSpawning; } }

    /* [Header("Blue enemies info")]
     [SerializeField] int blueWaveIndex;
     [SerializeField] Transform blueSpawnPoint;
     bool isBlueWaveSpawning = false;*/

    [Header("General info")]
    [SerializeField] TextMeshProUGUI wavesAmount;
    [SerializeField] GameManager gameManager;
    [SerializeField] EnemyChecker enemyChecker;
    [SerializeField] GameObject blueSpawnerCountdown;
    [SerializeField] WavesCountdown wavesCountdown;
    [SerializeField] BlueEnemySpawner blueSpawner;

    private int enemiesAlive = 0;
    private float currentCountdownTime;

    WaitForSeconds redWFS;
    // WaitForSeconds blueWFS;

    private void Awake()
    {
        redWFS = new WaitForSeconds(1);
        //  blueWFS = new WaitForSeconds(1);
    }

    public void StartRedWaves()
    {
        StartCoroutine(StartRedSpawner());
    }

    IEnumerator StartRedSpawner()
    {
        while (redWaveIndex < waves.Length && !gameManager.GameOver)
        {
            wavesAmount.text = $"Wave {redWaveIndex + 1}/10";
            if (redWaveIndex == 2)
            {
                blueSpawner.StartBlueWaves();
            }
            else if (redWaveIndex == 1)
            {
                blueSpawnerCountdown.SetActive(true);
            }
            WaveRed wave = waves[redWaveIndex];
            isRedWaveSpawning = true;
            for (int i = 0; i < wave.redEnemiesAmountInWave; i++)
            {
                if (gameManager.GameOver)
                {
                    yield break;
                }
                GameObject tempRedEnemy = wave.redEnemyPool.GetObject();
                tempRedEnemy.GetComponent<RedEnemy>().Initialize(wave.redEnemyPool);
                tempRedEnemy.transform.position = redSpawnPoint.position;
                enemiesAlive++;
                yield return redWFS;
            }
            isRedWaveSpawning = false;
            while (blueSpawner.IsBlueWaveSpawning)
            {
                yield return null;
            }
            yield return StartCoroutine(wavesCountdown.Countdown());
            redWaveIndex++;
            wavesAmount.text = $"Wave {redWaveIndex}/10";
        }
    }
    /*IEnumerator BlueEnemySpawner()
    {
        while (blueWaveIndex < waves.Length && !gameManager.GameOver)
        {
            Wave wave = waves[blueWaveIndex];
            isBlueWaveSpawning = true;
            for (int i = 0; i < wave.blueEnemiesAmountInWave; i++)
            {
                if (gameManager.GameOver)
                {
                    yield break;
                }

                GameObject tempBlueEnemy = wave.blueEnemyPool.GetObject();
                tempBlueEnemy.GetComponent<BlueEnemy>().Initialize(wave.blueEnemyPool);
                tempBlueEnemy.transform.position = blueSpawnPoint.position;
                enemiesAlive++;
                yield return blueWFS;
            }
            isBlueWaveSpawning = false;
            while (isRedWaveSpawning)
            {
                yield return null;
            }
            yield return StartCoroutine(Countdown(timeBetweenWaves));
            blueWaveIndex++;
        }
    }*/

    public void OnEnemyDestroyed(GameObject enemy)
    {
        enemiesAlive--;
        enemy.GetComponent<RedEnemy>().Die();
        if (redWaveIndex == waves.Length - 10 && enemiesAlive <= 2)
        {
            enemyChecker.CheckForRemainingEnemies();
        }
    }
}
