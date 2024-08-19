using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestWaveText;
    [SerializeField] TextMeshProUGUI currentWave;
    [SerializeField] RedEnemySpawner redSpawner;

    private void Start()
    {
        currentWave.text = "Wave: 0";
        bestWaveText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    public void WaveNumber (int waveNumber)
    {
        currentWave.text = $"Wave: {waveNumber}";
        if (waveNumber > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt ("HighScore", waveNumber);
            bestWaveText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        bestWaveText.text = "Best: 0";
    }
}
