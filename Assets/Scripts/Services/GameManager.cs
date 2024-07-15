using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject youWinMenu;
    [SerializeField] RedEnemySpawner enemySpawner;
    [SerializeField] TextMeshProUGUI wavesCounterTextGO;
    [SerializeField] TextMeshProUGUI wavesCounterTextWin;
    [SerializeField] SceneFader sceneFader;
    [SerializeField] AudioSource cameraAudioSource;
    [SerializeField] AudioSource endPoinAudioSource;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip winSound;

    string mainMenuName = "MainMenu";
    public int Rounds;
    bool gameOver = false;
    public bool GameOver { get { return gameOver; } }


    void Start()
    {
        Rounds = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            EndGame();
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        sceneFader.FadeTo(mainMenuName);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseToggle()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        Invoke(nameof(ReloadSceneDelayed), 0.1f);
    }
    private void ReloadSceneDelayed()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        gameOver = true;
        enemySpawner.enabled = false;
        cameraAudioSource.Stop();
        endPoinAudioSource.PlayOneShot(gameOverSound);
        wavesCounterTextGO.text = enemySpawner.RedWaveIndex.ToString();
        gameOverUI.SetActive(true);
        StartCoroutine(PauseGame());
    }
    private IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
    }

    public void YouWin()
    {
        gameOver = true;
        cameraAudioSource.Stop();
        endPoinAudioSource.PlayOneShot(winSound);
        youWinMenu.SetActive(true);
        wavesCounterTextWin.text = enemySpawner.RedWaveIndex.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
