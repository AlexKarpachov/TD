using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int startingBalance = 150;

    [SerializeField] int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    

    private void Awake()
    {
        currentBalance = startingBalance;
        UpdateMoney();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateMoney();
    }

    public void Withdrawal(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateMoney();

        if (currentBalance < 0)
        {
            //Loose the game
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void UpdateMoney()
    {
        scoreText.text = "Money: " + currentBalance.ToString();
    }
}
