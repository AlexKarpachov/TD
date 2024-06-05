using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    [SerializeField] int cost = 75;
    public bool CreatePrefabOld(Ballista ballista, Vector3 position)
    {
        BankOld bank = FindObjectOfType<BankOld>();

        if(bank == null)
        {
            return false;
        }

        if(bank.CurrentBalanceOld >= cost)
        {
            Instantiate(ballista, position, Quaternion.identity);
            bank.WithdrawalOld(cost);
            return true;
        }
        return false;
    }
}
