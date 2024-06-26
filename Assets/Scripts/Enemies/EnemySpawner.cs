using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    [Header("Red enemies info")]
    [SerializeField] int redWaveIndex = 0;
    public int RedWaveIndex { get { return redWaveIndex; } }
    [SerializeField] Transform redSpawnPoint;
    bool isRedWaveSpawning = false;

    [Header("Blue enemies info")]
    [SerializeField] int blueWaveIndex;
    [SerializeField] Transform blueSpawnPoint;
    bool isBlueWaveSpawning = false;

    [Header("General info")]
    [SerializeField] Transform endPoint;
    [SerializeField] float timeBetweenWaves = 5.9f;
    [SerializeField] float startDelayTime = 4f;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] GameManager gameManager;

    private int enemiesAlive = 0;

    void Start()
    {
        StartCoroutine(StartupCountdown());
    }

    IEnumerator StartupCountdown()
    {
        while (startDelayTime > 0)
        {
            startDelayTime -= Time.deltaTime;
            countdownText.text = "The game will start in " + Mathf.Round(startDelayTime).ToString();
            yield return null;
        }
        StartGame();
    }

    void StartGame()
    {
        StartCoroutine(RedEnemySpawner());
    }

    IEnumerator RedEnemySpawner()
    {
        while (redWaveIndex < waves.Length && !gameManager.GameOver)
        {
            if (redWaveIndex == 2)
            {
                StartCoroutine(BlueEnemySpawner());
            }
            Wave wave = waves[redWaveIndex];
            isRedWaveSpawning = true;
            for (int i = 0; i < wave.redEnemiesAmountInWave; i++)
            {
                if (gameManager.GameOver)
                {
                    yield break;
                }
                GameObject tempRedEnemy = Instantiate(wave.redEnemyPrefab, redSpawnPoint.position, Quaternion.identity);
                EnemyMover tempLogic = tempRedEnemy.GetComponent<EnemyMover>();
                tempLogic.MoveTo(endPoint);
                enemiesAlive++;
                Debug.Log(enemiesAlive);
                yield return new WaitForSeconds(1 / wave.timeBetweenRedEnemies);
            }
            isRedWaveSpawning = false;
            while (isBlueWaveSpawning)
            {
                yield return null;
            }
            yield return StartCoroutine(Countdown(timeBetweenWaves));
            redWaveIndex++;
        }
    }
    // створити додаткових ворогів та налаштувати хвилі
    IEnumerator BlueEnemySpawner()
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

                GameObject tempBlueEnemy = Instantiate(wave.blueEnemyPrefab, blueSpawnPoint.position, Quaternion.identity);
                EnemyMover tempLogic = tempBlueEnemy.GetComponent<EnemyMover>();
                tempLogic.MoveTo(endPoint);
                enemiesAlive++;
                Debug.Log(enemiesAlive);
                yield return new WaitForSeconds(1 / wave.timeBetweenBlueEnemies);
            }
            isBlueWaveSpawning = false;
            while (isRedWaveSpawning)
            {
                yield return null;
            }
            yield return StartCoroutine(Countdown(timeBetweenWaves));
            blueWaveIndex++;
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
                countdownText.text = "Next wave in " + Mathf.Round(time).ToString() + " sec";
                yield return null;
            }
        }
        else if (redWaveIndex == waves.Length - 1)
        {
            countdownText.text = "This is the last wave";
        }
    }
    public void OnEnemyDestroyed()
    {
        enemiesAlive--;
        Debug.Log(enemiesAlive);
        if (redWaveIndex == waves.Length && enemiesAlive == 0)
        {
            gameManager.YouWin();
            this.enabled = false;
        }
    }
}