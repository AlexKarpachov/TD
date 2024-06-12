using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TextMeshProUGUI wavesCounterText;

    public int Rounds;

    bool gameOver = false;
    public bool GameOver { get { return gameOver; } }

    private void OnEnable()
    {
        //wavesCounterText.text = enemySpawner.redWaveNumber.ToString();
    }

    void Start()
    {
        Rounds = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void EndGame()
    {
        wavesCounterText.text = enemySpawner.redWaveNumber.ToString();
        gameOverUI.SetActive(true);
        gameOver = true;
      //  Time.timeScale = 0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
