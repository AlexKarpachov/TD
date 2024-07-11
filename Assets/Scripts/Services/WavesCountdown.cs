using System.Collections;
using TMPro;
using UnityEngine;

public class WavesCountdown : MonoBehaviour
{
    [SerializeField] float timeBetweenWaves = 5.9f;
    [SerializeField] float startDelayTime = 4f;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI wavesAmount;
    [SerializeField] GameObject blueSpawnerCountdown;
    [SerializeField] EnemySpawner redSpawner;
    [SerializeField] GameManager gameManager;

    private float currentCountdownTime;

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
        redSpawner.StartRedWaves();
    }

    public IEnumerator Countdown()
    {
        if (redSpawner.RedWaveIndex < redSpawner.waves.Length - 1)
        {
            countdownText.text = Mathf.Round(timeBetweenWaves).ToString();
            while (timeBetweenWaves > 0 && !gameManager.GameOver)
            {
                timeBetweenWaves -= Time.deltaTime;
                currentCountdownTime = timeBetweenWaves;
                countdownText.text = "Next wave in " + Mathf.Round(timeBetweenWaves).ToString() + " sec";
                yield return null;
            }
        }
        else if (redSpawner.RedWaveIndex == redSpawner.waves.Length - 1)
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
}
