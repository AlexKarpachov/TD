using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoneyCalculator : MonoBehaviour
{
    [SerializeField] int moneyReward = 25;
    [SerializeField] int moneyPenaly = 25;

    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void MoneyDeposit()
    {
        if (bank == null) { return; }
        bank.Deposit(moneyReward);
    }

    public void MoneyWithdraw()
    {
        if (bank == null) { return; }
        bank.FundsWithdrawals(moneyPenaly);
    }
}
