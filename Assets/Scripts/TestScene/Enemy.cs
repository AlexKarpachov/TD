using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldRewardOld = 25;
    [SerializeField] int goldPenaltyOld = 25;

    BankOld bank;

    private void Start()
    {
        bank = FindObjectOfType<BankOld>();
    }

    public void RewardGoldOld()
    {
        if (bank == null) { return; }
        bank.DepositOld(goldRewardOld);
    }

    public void PenaltyGoldOld()
    {
        if (bank == null) { return; }
        bank.WithdrawalOld(goldPenaltyOld);
    }
}
