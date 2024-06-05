using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] TowerBuilder towerBuilder;
    [SerializeField] GameObject storeUI;
    [SerializeField] Raycast raycast;
    [SerializeField] Bank bank;
    [SerializeField] int smallArcherTowerCost = 10;
    [SerializeField] int largeTowerArcherCost = 20;
    [SerializeField] int smallMageTowerCost = 40;
    [SerializeField] int largeMageTowerCost = 70;

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
            Debug.Log("Not enough money");
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
            Debug.Log("Not enough money");
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
            Debug.Log("Not enough money");
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
            Debug.Log("Not enough money");
        }
    }
}
