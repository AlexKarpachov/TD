using System.Collections;
using UnityEngine;

public class BlueEnemySpawner : MonoBehaviour
{
    public WaveBlue[] waves;
    [Header("Blue enemies info")]
    [SerializeField] int blueWaveIndex = 0;
    [SerializeField] Transform blueSpawnPoint;
    bool isBlueWaveSpawning = false;
    public bool IsBlueWaveSpawning { get { return isBlueWaveSpawning; } }

    [Header("General info")]
    [SerializeField] GameManager gameManager;
    [SerializeField] EnemyChecker enemyChecker;
    [SerializeField] RedEnemySpawner redSpawner;
    [SerializeField] RedEnemySpawner wavesCountdown;

    WaitForSeconds blueWFS;

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

            //Spawn spearmen
            for (int i = 0; i < wave.blueSpearmanInWave; i++)
            {
                if (gameManager.GameOver)
                {
                    yield break;
                }

                GameObject tempBlueEnemy = wave.blueSpearmanPool.GetObject();
                tempBlueEnemy.GetComponent<BlueEnemy>().Initialize(wave.blueSpearmanPool);
                tempBlueEnemy.transform.position = blueSpawnPoint.position;
                EnemyChecker.enemiesAlive++;
                yield return blueWFS;
            }
            //Spawn swordmen
            for (int i = 0; i < wave.blueSwordmanInWave; i++)
            {
                if (gameManager.GameOver)
                {
                    yield break;
                }

                GameObject tempBlueEnemy = wave.blueSwordmanPool.GetObject();
                tempBlueEnemy.GetComponent<BlueSwordman>().Initialize(wave.blueSwordmanPool);
                tempBlueEnemy.transform.position = blueSpawnPoint.position;
                EnemyChecker.enemiesAlive++;
                yield return blueWFS;
            }

            isBlueWaveSpawning = false;
            while (redSpawner.IsRedWaveSpawning)
            {
                yield return null;
            }
            yield return wavesCountdown.StartCountdown();
            blueWaveIndex++;
        }
    }
}
