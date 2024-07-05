using System.Collections;
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

    WaitForSeconds noMoneyMethod;

    private void Awake()
    {
        noMoneyMethod = new WaitForSeconds(2);
    }

    public void SelectTowerToBuild(GameObject towerPrefab, int cost)
    {
        TowerBuildPoint towerBuildPointScript = raycast.GetTowerBuildPointScript();
        storeUI.SetActive(false);
        Time.timeScale = 1f;
        towerBuilder.SetTowerToBuild(towerPrefab);

        if (towerBuilder.Construct(towerBuildPointScript))
        {
            towerBuildPointScript.ChangeBuildingPermission(false);
            bank.FundsWithdrawals(cost);
        }
    }

    public void Archer1TowerPurchase()
    {
        if (bank.CurrentBalance >= smallArcherTowerCost)
        {
            SelectTowerToBuild(towerBuilder.archerTowerPrefab, smallArcherTowerCost);
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
            SelectTowerToBuild(towerBuilder.archer2TowerPrefab, largeTowerArcherCost);
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
            SelectTowerToBuild(towerBuilder.mageTowerPrefab, smallMageTowerCost);
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
            SelectTowerToBuild(towerBuilder.mage2TowerPrefab, largeMageTowerCost);
        }
        else
        {
            StartCoroutine(NoMoney());
        }
    }
    IEnumerator NoMoney()
    {
        noMoneyText.SetActive(true);
        yield return noMoneyMethod;
        noMoneyText.SetActive(false);
    }
}
