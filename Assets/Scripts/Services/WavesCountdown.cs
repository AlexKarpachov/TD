using System.Collections;
using TMPro;
using UnityEngine;

public class WavesCountdown : MonoBehaviour
{
    [SerializeField] float startDelayTime = 4f;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] RedEnemySpawner redSpawner;

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
}
