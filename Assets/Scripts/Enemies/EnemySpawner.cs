using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int redWaveNumber;
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
    [SerializeField] GameManager gameManager;

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
        StartCoroutine(BlueEnemySpawner());
    }

    IEnumerator RedEnemySpawner()
    {
        if (gameManager.GameOver)
        {
            StopAllCoroutines();
            yield break;
        }

        while (true && !gameManager.GameOver)
        {
            int needSpawn = redWaveNumber * redEnemiesInWave;

            while (needSpawn > 0 && !gameManager.GameOver)
            {
                GameObject tempRedEnemy = Instantiate(redEnemyPrefab, redSpawnPoint.position, Quaternion.identity);
                EnemyMover tempLogic = tempRedEnemy.GetComponent<EnemyMover>();

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
        if (gameManager.GameOver)
        {
            StopAllCoroutines();
            yield break;
        }

        while (true && !gameManager.GameOver)
        {
            int needSpawn = blueWaveNumber * blueEnemiesInWave;

            while (needSpawn > 0 && !gameManager.GameOver)
            {
                GameObject tempBlueEnemy = Instantiate(blueEnemyPrefab, blueSpawnPoint.position, Quaternion.identity);
                EnemyMover tempLogic = tempBlueEnemy.GetComponent<EnemyMover>();
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
        while (time > 0 && !gameManager.GameOver)
        {
            time -= Time.deltaTime;
            countdownText.text = "Next wave in " + Mathf.Round(time).ToString() + " sec";
            yield return null;
        }
    }
}