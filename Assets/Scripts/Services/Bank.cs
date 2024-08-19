using TMPro;
using UnityEngine;

// manages a player's bank balance
// The current balance is displayed on the screen using a TextMeshProUGUI component.
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

    // adds the specified amount to the currentBalance. The Mathf.Abs function ensures that the amount is always positive.
    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
    }

    // subtracts the specified amount from the currentBalance. The Mathf.Abs function ensures that the amount is always positive.
    public void FundsWithdrawals(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
    }
}
