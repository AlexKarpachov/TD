using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;

    bool gameOver = false;
    public bool GameOver { get { return gameOver; } }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void EndGame()
    {
        gameOverUI.SetActive(true);
        gameOver = true;
        Time.timeScale = 0f;
    }
}
