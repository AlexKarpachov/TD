using TMPro;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHPText;
    [SerializeField] int startLives = 3;
    [SerializeField] RedEnemySpawner enemySpawner;
    [SerializeField] GameManager gameManager;

    public static int Lives;

    private void Start()
    {
        Lives = startLives;
    }

    private void Update()
    {
        playerHPText.text = "Lives: " + Lives;
    }

    public void OutOfLives()
    {
        Lives--;
        if (Lives <= 0)
        {
            gameManager.EndGame();
        }
    }
}
