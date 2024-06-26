using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject youWinMenu;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TextMeshProUGUI wavesCounterTextGO;
    [SerializeField] TextMeshProUGUI wavesCounterTextWin;
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
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        wavesCounterTextGO.text = enemySpawner.RedWaveIndex.ToString();
        gameOverUI.SetActive(true);
        gameOver = true;
    }
    public void YouWin()
    {
        youWinMenu.SetActive(true);
        wavesCounterTextWin.text = enemySpawner.RedWaveIndex.ToString();
        gameOver = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
