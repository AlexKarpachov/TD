using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TextMeshProUGUI wavesCounterText;
    [SerializeField] SceneFader sceneFader;

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
    }


    public void MainMenu()
    {
        PauseToggle();
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
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        wavesCounterText.text = enemySpawner.redWaveNumber.ToString();
        gameOverUI.SetActive(true);
        gameOver = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
