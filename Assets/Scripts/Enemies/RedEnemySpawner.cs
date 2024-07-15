using System.Collections;
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

    [Header("General info")]
    [SerializeField] TextMeshProUGUI wavesAmount;
    [SerializeField] GameManager gameManager;
    [SerializeField] EnemyChecker enemyChecker;
    [SerializeField] GameObject blueSpawnerCountdown;
    [SerializeField] WavesCountdown wavesCountdown;
    [SerializeField] BlueEnemySpawner blueSpawner;
    [SerializeField] float timeBetweenWaves = 5.9f;
    [SerializeField] TextMeshProUGUI countdownText;

    private float currentCountdownTime;

    WaitForSeconds redWFS;

    private void Awake()
    {
        redWFS = new WaitForSeconds(1);
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
            // Spawn spearmen
            for (int i = 0; i < wave.redSpearmenAmountInWave; i++)
            {
                if (gameManager.GameOver)
                {
                    yield break;
                }

                GameObject tempRedEnemy = wave.redSpearmanPool.GetObject();
                tempRedEnemy.GetComponent<RedSpearman>().Initialize(wave.redSpearmanPool);
                tempRedEnemy.transform.position = redSpawnPoint.position;
                EnemyChecker.enemiesAlive++;
                yield return redWFS;
            }

            // Spawn swordmen
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
            while (blueSpawner.IsBlueWaveSpawning)
            {
                yield return null;
            }
            yield return StartCoroutine(Countdown(timeBetweenWaves));
            redWaveIndex++;
            wavesAmount.text = $"Wave {redWaveIndex}/10";
        }
    }
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
