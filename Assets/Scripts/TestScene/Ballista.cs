using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    [SerializeField] int cost = 75;
    public bool CreatePrefab(Ballista ballista, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(bank == null)
        {
            return false;
        }

        if(bank.CurrentBalance >= cost)
        {
            Instantiate(ballista, position, Quaternion.identity);
            bank.Withdrawal(cost);
            return true;
        }
        return false;
    }
}
