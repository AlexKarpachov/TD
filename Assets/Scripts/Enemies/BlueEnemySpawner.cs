using System.Collections;
using UnityEngine;

public class BlueEnemySpawner : MonoBehaviour
{
    public WaveBlue[] waves; // represent the waves of enemies to be spawned
    [Header("Blue enemies info")]
    [SerializeField] int blueWaveIndex = 0;
    [SerializeField] Transform blueSpawnPoint;
    bool isBlueWaveSpawning = false;
    public bool IsBlueWaveSpawning { get { return isBlueWaveSpawning; } }

    [Header("General info")]
    [SerializeField] GameManager gameManager;
    [SerializeField] EnemyChecker enemyChecker;
    [SerializeField] RedEnemySpawner redSpawner;

    WaitForSeconds blueWFS;

    private void Awake()
    {
        blueWFS = new WaitForSeconds(1);
    }

    public void StartBlueWaves()
    {
        StartCoroutine(StartEnemySpawner());
    }

    // responsible for spawning blue enemies in waves, with a delay between each wave
    IEnumerator StartEnemySpawner()
    {
        while (blueWaveIndex < waves.Length && !gameManager.GameOver)
        {
            // retrieves the current wave configuration from the waves array using the blueWaveIndex
            WaveBlue wave = waves[blueWaveIndex];
            isBlueWaveSpawning = true;

            /*Spawn spearmen
             * spawns blueSpearmanInWave number of spearmen enemies, one at a time, 
             * with a short delay between each spawn (using yield return blueWFS;). 
             * Each spawned enemy is initialized with the redSpearmanPool and positioned at the redSpawnPoint.
             * */
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
                Debug.Log("enemies number is " + EnemyChecker.enemiesAlive);
                yield return blueWFS;
            }

            /* Spawn swordmen
             * spawns blueSwordmanInWave number of swordmen enemies, one at a time, with a short delay between each spawn.
             * */
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
                Debug.Log("enemies number is " + EnemyChecker.enemiesAlive);
                yield return blueWFS;
            }

            isBlueWaveSpawning = false;

            // If the red spawner is currently spawning enemies, it waits until the red spawner is finished.
            while (redSpawner.IsRedWaveSpawning)
            {
                yield return null;
            }
            yield return redSpawner.StartCountdown();
            blueWaveIndex++;
        }
    }
}
