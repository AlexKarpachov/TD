using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int redWaveNumber;
    [SerializeField] int redEnemiesInWave;
    [SerializeField] GameObject redEnemyPrefab;
    [SerializeField] Transform redSpawnPoint;

    [SerializeField] int blueWaveNumber;
    [SerializeField] int blueEnemiesInWave;
    [SerializeField] GameObject blueEnemyPrefab;
    [SerializeField] Transform blueSpawnPoint;

    [SerializeField] Transform endPoint;
    [SerializeField] float timeBetweenWaves = 5.9f;
    [SerializeField] float startDelayTime = 4f;
    [SerializeField] TextMeshProUGUI countdownText;

    public bool gameOver = false;

    void Start()
    {
        StartCoroutine(StartupCountdown());
    }

    IEnumerator StartupCountdown()
    {
        countdownText.text = "The game will start in " + Mathf.Round(startDelayTime).ToString();
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
        StartCoroutine(BlueEnemySpawner());
    }

    IEnumerator RedEnemySpawner()
    {
        if (gameOver)
        {
            StopAllCoroutines();
            yield break;
        }

        while (true)
        {
            int needSpawn = redWaveNumber * redEnemiesInWave;

            while (needSpawn > 0 && !gameOver)
            {
                GameObject tempRedEnemy = Instantiate(redEnemyPrefab, redSpawnPoint.position, Quaternion.identity);
                EnemyLogic tempLogic = tempRedEnemy.GetComponent<EnemyLogic>();

                tempLogic.MoveTo(endPoint);
                needSpawn--;
                yield return new WaitForSeconds(1);
            }

            yield return StartCoroutine(Countdown(timeBetweenWaves));
            redWaveNumber++;
        }
    }

    IEnumerator BlueEnemySpawner()
    {
        if (gameOver)
        {
            StopAllCoroutines();
            yield break;
        }

        while (true)
        {
            int needSpawn = blueWaveNumber * blueEnemiesInWave;

            while (needSpawn > 0 && !gameOver)
            {
                GameObject tempBlueEnemy = Instantiate(blueEnemyPrefab, blueSpawnPoint.position, Quaternion.identity);
                EnemyLogic tempLogic = tempBlueEnemy.GetComponent<EnemyLogic>();
                tempLogic.MoveTo(endPoint);
                needSpawn--;
                yield return new WaitForSeconds(1);
            }

            yield return StartCoroutine(Countdown(timeBetweenWaves));
            blueWaveNumber++;
        }
    }

    IEnumerator Countdown(float time)
    {
        countdownText.text = Mathf.Round(time).ToString();
        while (time > 0)
        {
            time -= Time.deltaTime;
            countdownText.text = "Next wave in " + Mathf.Round(time).ToString() + " sec";
            yield return null;
        }
    }
}