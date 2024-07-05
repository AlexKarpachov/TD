using TMPro;
using Unity.AI.Navigation;
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
    [SerializeField] AudioSource cameraAudioSource;
    [SerializeField] AudioSource endPoinAudioSource;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] GameObject navigationRoute;

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
        gameOver = true;
        enemySpawner.enabled = false;
        navigationRoute.GetComponent<NavMeshSurface>().enabled = false;
        cameraAudioSource.Stop();
        endPoinAudioSource.PlayOneShot(gameOverSound);
        wavesCounterTextGO.text = enemySpawner.RedWaveIndex.ToString();
        gameOverUI.SetActive(true);
    }

    public void YouWin()
    {
        gameOver = true;
        navigationRoute.GetComponent<NavMeshSurface>().enabled = false;
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
