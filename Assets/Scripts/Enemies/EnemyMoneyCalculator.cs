using UnityEngine;

// manages the money reward and penalty system for enemies
public class EnemyMoneyCalculator : MonoBehaviour
{
    [SerializeField] int moneyReward = 25;
    [SerializeField] int moneyPenaly = 25;

    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    //  deposits the moneyReward amount into the Bank object
    public void MoneyDeposit()
    {
        if (bank == null) { return; }
        bank.Deposit(moneyReward);
    }

    // withdraws the moneyPenalty amount from the Bank object. 
    public void MoneyWithdraw()
    {
        if (bank == null) { return; }
        bank.FundsWithdrawals(moneyPenaly);
    }
}
