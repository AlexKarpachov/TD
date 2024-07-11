using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlueEnemySpawner : MonoBehaviour
{
    public WaveBlue[] waves;
    [Header("Blue enemies info")]
    [SerializeField] int blueWaveIndex;
    [SerializeField] Transform blueSpawnPoint;
    bool isBlueWaveSpawning = false;
    public bool IsBlueWaveSpawning { get { return isBlueWaveSpawning; } }

    [Header("General info")]
    [SerializeField] GameManager gameManager;
    [SerializeField] EnemyChecker enemyChecker;
    [SerializeField] RedEnemySpawner redSpawner;
    [SerializeField] WavesCountdown wavesCountdown;

    WaitForSeconds blueWFS;
    private int enemiesAlive = 0;

    private void Awake()
    {
        blueWFS = new WaitForSeconds(1);
    }

    public void StartBlueWaves()
    {
        StartCoroutine(StartEnemySpawner());
    }

    IEnumerator StartEnemySpawner()
    {
        while (blueWaveIndex < waves.Length && !gameManager.GameOver)
        {
            WaveBlue wave = waves[blueWaveIndex];
            isBlueWaveSpawning = true;
            for (int i = 0; i < wave.blueEnemiesAmountInWave; i++)
            {
                if (gameManager.GameOver)
                {
                    yield break;
                }

                GameObject tempBlueEnemy = wave.blueEnemyPool.GetObject();
               // tempBlueEnemy.GetComponent<BlueEnemy>().Initialize(wave.blueEnemyPool);
                tempBlueEnemy.transform.position = blueSpawnPoint.position;
                enemiesAlive++;
                yield return blueWFS;
            }
            isBlueWaveSpawning = false;
            while (redSpawner.IsRedWaveSpawning)
            {
                yield return null;
            }
            yield return StartCoroutine(wavesCountdown.Countdown());
            blueWaveIndex++;
        }
    }

    public void OnEnemyDestroyed(GameObject enemy)
    {
        enemiesAlive--;
        enemy.GetComponent<RedEnemy>().Die();
        if (redSpawner.RedWaveIndex == waves.Length - 10 && enemiesAlive <= 2)
        {
            enemyChecker.CheckForRemainingEnemies();
        }
    }
}
