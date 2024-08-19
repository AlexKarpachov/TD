using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject youWinMenu;
    [SerializeField] GameObject storeUI;
    [SerializeField] Store store;
    [SerializeField] RedEnemySpawner redEnemySpawner;
    [SerializeField] TextMeshProUGUI wavesCounterTextGameOver;
    [SerializeField] TextMeshProUGUI wavesCounterTextWin;
    [SerializeField] SceneFader sceneFader;
    [SerializeField] AudioSource cameraAudioSource;
    [SerializeField] AudioSource endPoinAudioSource;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] GameSpeed gameSpeed;

    string mainMenuName = "MainMenu";
    const float reloadSceneDelay = 0.2f;
    const float pauseGameDelay = 0.5f;
    bool gameOver = false;
    public bool GameOver { get { return gameOver; } }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
            storeUI.SetActive(false);
            store.ColliderEnabled();
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
        GameSpeed.isSpeedOn = false;
        gameSpeed.UpdateButtonIcon();
    }

    // Resumes the game from a paused state.
    public void ResumeGame()
    {
        GameSpeed.isSpeedOn = false;
        gameSpeed.UpdateButtonIcon();
        Time.timeScale = 1f;
    }

    // Toggles the pause menu on and off.
    public void PauseToggle()
    {
        if (!gameOver)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (pauseMenu.activeSelf)
            {
                GameSpeed.isSpeedOn = false;
                gameSpeed.UpdateButtonIcon();
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
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
        cameraAudioSource.Stop();
        youWinMenu.SetActive(true);
        wavesCounterTextWin.text = redEnemySpawner.RedWaveIndex.ToString();
        endPoinAudioSource.PlayOneShot(winSound);
        gameOver = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
