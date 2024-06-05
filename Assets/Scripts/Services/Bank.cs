using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] PlayerLives playerLives;
    [SerializeField] int startingBalance = 10;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI balanceText;
    public int CurrentBalance { get { return currentBalance; } }

    void Awake()
    {
        currentBalance = startingBalance;
    }

    private void Update()
    {
        balanceText.text = "Balance: $" + currentBalance;
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
    }

    public void FundsWithdrawals(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
    }
}
