using System.Collections;
using TMPro;
using UnityEngine;

/* manages a countdown timer that precedes the start of a game. 
 * The countdown is displayed on a TextMeshProUGUI component, and once the countdown reaches zero, 
 * it triggers the start of the game by calling the StartRedWaves method on the RedEnemySpawner script.
 */
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
