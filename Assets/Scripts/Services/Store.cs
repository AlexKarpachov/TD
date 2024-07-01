using System.Collections;
using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] TowerBuilder towerBuilder;
    [SerializeField] GameObject storeUI;
    [SerializeField] GameObject noMoneyText;
    [SerializeField] Raycast raycast;
    [SerializeField] Bank bank;

    [SerializeField] int smallArcherTowerCost = 10;
    public int SmallArcherTowerCost { get { return smallArcherTowerCost; } }
    [SerializeField] int largeTowerArcherCost = 20;
    public int LargeArcherTowerCost { get { return largeTowerArcherCost; } }
    [SerializeField] int smallMageTowerCost = 40;
    public int SmallMageTowerCost { get { return smallMageTowerCost; } }
    [SerializeField] int largeMageTowerCost = 70;
    public int LargeMageTowerCost { get { return largeMageTowerCost; } }

    public void SelectTowerToBuild(GameObject towerPrefab)
    {
        storeUI.SetActive(false);
        Time.timeScale = 1f;
        towerBuilder.SetTowerToBuild(towerPrefab);
        TowerBuildPoint towerBuildPointScript = raycast.GetTowerBuildPointScript();

        if (towerBuilder.Construct(towerBuildPointScript))
        {
            towerBuildPointScript.ChangeBuildingPermission(false);
        }
    }

    public void Archer1TowerPurchase()
    {
        if (bank.CurrentBalance >= smallArcherTowerCost)
        {
            SelectTowerToBuild(towerBuilder.archerTowerPrefab);
            bank.FundsWithdrawals(smallArcherTowerCost);
        }
        else
        {
            StartCoroutine(NoMoney());
        }
        
    }
    public void Archer2TowerPurchase()
    {
        if (bank.CurrentBalance >= largeTowerArcherCost)
        {
            SelectTowerToBuild(towerBuilder.archer2TowerPrefab);
            bank.FundsWithdrawals(largeTowerArcherCost);
        }
        else
        {
            StartCoroutine(NoMoney());
        }
    }
    public void Mage1TowerPurchase()
    {
        if (bank.CurrentBalance >= smallMageTowerCost)
        {
            SelectTowerToBuild(towerBuilder.mageTowerPrefab);
            bank.FundsWithdrawals(smallMageTowerCost);
        }
        else
        {
            StartCoroutine(NoMoney());
        }
    }
    public void Mage2TowerPurchase()
    {
        if (bank.CurrentBalance >= largeMageTowerCost)
        {
            SelectTowerToBuild(towerBuilder.mage2TowerPrefab);
            bank.FundsWithdrawals(largeMageTowerCost);
        }
        else
        {
            StartCoroutine(NoMoney());
        }
    }
    IEnumerator NoMoney()
    {
        noMoneyText.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        noMoneyText.SetActive(false);
    }
}
