using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject youWinMenu;
    [SerializeField] RedEnemySpawner redEnemySpawner;
    [SerializeField] TextMeshProUGUI wavesCounterTextGameOver;
    [SerializeField] TextMeshProUGUI wavesCounterTextWin;
    [SerializeField] SceneFader sceneFader;
    [SerializeField] AudioSource cameraAudioSource;
    [SerializeField] AudioSource endPoinAudioSource;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip winSound;

    string mainMenuName = "MainMenu";
   // public int Rounds;
    const float reloadSceneDelay = 0.2f;
    const float pauseGameDelay = 0.5f;
    bool gameOver = false;
    public bool GameOver { get { return gameOver; } }


    /*void Start()
    {
      Rounds = 0;
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            EndGame();
        }
    }

    // Returns to the main menu.
    public void MainMenu()
    {
        Time.timeScale = 1f;
        sceneFader.FadeTo(mainMenuName);
    }

    // Resumes the game from a paused state.
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    // Toggles the pause menu on and off.
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

    // reloads the current scene, but with a delay. 
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        Invoke(nameof(ReloadSceneDelayed), reloadSceneDelay);
    }
    private void ReloadSceneDelayed()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        gameOver = true;
        redEnemySpawner.enabled = false;
        cameraAudioSource.Stop();
        endPoinAudioSource.PlayOneShot(gameOverSound);
        wavesCounterTextGameOver.text = redEnemySpawner.RedWaveIndex.ToString();
        gameOverUI.SetActive(true);
        StartCoroutine(PauseGame());
    }
    private IEnumerator PauseGame()
    {
        yield return new WaitForSeconds(pauseGameDelay);
        Time.timeScale = 0f;
    }

    public void YouWin()
    {
        gameOver = true;
        cameraAudioSource.Stop();
        endPoinAudioSource.PlayOneShot(winSound);
        youWinMenu.SetActive(true);
        wavesCounterTextWin.text = redEnemySpawner.RedWaveIndex.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
