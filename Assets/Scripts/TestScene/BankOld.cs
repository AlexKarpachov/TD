using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BankOld : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int startingBalanceOld = 150;

    [SerializeField] int currentBalanceOld;
    public int CurrentBalanceOld { get { return currentBalanceOld; } }

    

    private void Awake()
    {
        currentBalanceOld = startingBalanceOld;
        UpdateMoneyOld();
    }

    public void DepositOld(int amount)
    {
        currentBalanceOld += Mathf.Abs(amount);
        UpdateMoneyOld();
    }

    public void WithdrawalOld(int amount)
    {
        currentBalanceOld -= Mathf.Abs(amount);
        UpdateMoneyOld();

        if (currentBalanceOld < 0)
        {
            //Loose the game
            ReloadSceneOld();
        }
    }

    void ReloadSceneOld()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void UpdateMoneyOld()
    {
        scoreText.text = "Money: " + currentBalanceOld.ToString();
    }
}
