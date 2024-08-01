using System.Collections;
using TMPro;
using UnityEngine;

public class RedEnemySpawner : MonoBehaviour
{
    public WaveRed[] waves; // represent the waves of enemies to be spawned
    [Header("Red enemies info")]
    [SerializeField] Transform redSpawnPoint;
    [SerializeField] int redWaveIndex = 0;
    public int RedWaveIndex { get { return redWaveIndex; } }
    bool isRedWaveSpawning = false;
    public bool IsRedWaveSpawning { get { return isRedWaveSpawning; } }

    [Header("General info")]
    [SerializeField] TextMeshProUGUI wavesAmount;
    [SerializeField] GameManager gameManager;
    [SerializeField] EnemyChecker enemyChecker;
    [SerializeField] GameObject blueSpawnerCountdown;
    [SerializeField] WavesCountdown wavesCountdown;
    [SerializeField] BlueEnemySpawner blueSpawner;
    [SerializeField] float timeBetweenWaves = 5.9f;
    [SerializeField] TextMeshProUGUI countdownText;

    float currentCountdownTime;

    WaitForSeconds redWFS;

    private void Awake()
    {
        redWFS = new WaitForSeconds(1);
    }

    public void StartRedWaves()
    {
        StartCoroutine(StartRedSpawner());
    }

    // responsible for spawning red enemies in waves, with a delay between each wave
    IEnumerator StartRedSpawner()
    {
        while (redWaveIndex < waves.Length && !gameManager.GameOver)
        {
            // sets the wavesAmount text to display the current wave number
            wavesAmount.text = $"Wave {redWaveIndex + 1}/10";
            if (redWaveIndex == 2)
            {
                blueSpawner.StartBlueWaves();
            }
            else if (redWaveIndex == 1)
            {
                blueSpawnerCountdown.SetActive(true);
            }

            // retrieves the current wave configuration from the waves array using the redWaveIndex
            WaveRed wave = waves[redWaveIndex];

            isRedWaveSpawning = true;
            /*Spawn spearmen
             * spawns redSpearmenAmountInWave number of spearmen enemies, one at a time, 
             * with a short delay between each spawn (using yield return redWFS;). 
             * Each spawned enemy is initialized with the redSpearmanPool and positioned at the redSpawnPoint.
             * */
            for (int i = 0; i < wave.redSpearmenAmountInWave; i++)
            {
                if (gameManager.GameOver)
                {
                    yield break;
                }

                GameObject tempRedEnemy = wave.redSpearmanPool.GetSpearman();
                tempRedEnemy.GetComponent<RedSpearman>().Initialize(wave.redSpearmanPool);
                tempRedEnemy.transform.position = redSpawnPoint.position;
                EnemyChecker.enemiesAlive++;
                yield return redWFS;
            }

            /* Spawn swordmen
             * spawns redSwordmenAmountInWave number of swordmen enemies, one at a time, with a short delay between each spawn.
             * */
            for (int i = 0; i < wave.redSwordmenAmountInWave; i++)
            {
                if (gameManager.GameOver)
                {
                    yield break;
                }

                GameObject tempRedEnemy = wave.redSwordmanPool.GetObject();
                tempRedEnemy.GetComponent<RedSwordman>().InitializeSwordman(wave.redSwordmanPool);
                tempRedEnemy.transform.position = redSpawnPoint.position;
                EnemyChecker.enemiesAlive++;
                yield return redWFS;
            }

            isRedWaveSpawning = false;

            // If the blue spawner is currently spawning enemies, it waits until the blue spawner is finished.
            while (blueSpawner.IsBlueWaveSpawning)
            {
                yield return null;
            }
            // starts a countdown (using StartCoroutine(Countdown(timeBetweenWaves))) before the next wave
            yield return StartCoroutine(Countdown(timeBetweenWaves));
            redWaveIndex++;
            wavesAmount.text = $"Wave {redWaveIndex}/10";
        }
    }
    // displays a countdown timer until the next wave.
    IEnumerator Countdown(float time)
    {
        if (redWaveIndex < waves.Length - 1)
        {
            countdownText.text = Mathf.Round(time).ToString();
            while (time > 0 && !gameManager.GameOver)
            {
                time -= Time.deltaTime;
                currentCountdownTime = time;
                countdownText.text = "Next wave in " + Mathf.Round(time).ToString() + " sec";
                yield return null;
            }
        }
        else if (redWaveIndex == waves.Length - 1)
        {
            countdownText.text = "This is the last wave";
        }
    }
    public float RemainingCountdownTime()
    {
        return currentCountdownTime;
    }
    public float InitialCountdownTime()
    {
        return timeBetweenWaves;
    }

    public IEnumerator StartCountdown()
    {
        yield return Countdown(timeBetweenWaves);
    }
}
