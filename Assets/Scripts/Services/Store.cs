using System.Collections;
using UnityEngine;

// manages the store's UI, handles tower purchases, and manages the player's funds.
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

    BoxCollider[] buildPointColliders;

    // In start we collect all BuildPoint box colliders to enable them later.
    // We do so to exclude the possibility when the raycast goes through the Store UI to the BuildPoint, different from the chosen one.
    private void Start()
    {
        GameObject[] buildPoints = GameObject.FindGameObjectsWithTag("BuildPoint");
        buildPointColliders = new BoxCollider[buildPoints.Length];

        for (int i = 0; i < buildPoints.Length; i++)
        {
            buildPointColliders[i] = buildPoints[i].GetComponent<BoxCollider>();
        }
    }

    /* called when the player selects a tower to build. 
     * It takes two parameters: towerPrefab, which is the prefab of the tower to build, and cost, which is the cost of the tower. 
     * The method sets the tower builder's towerToBuild property to the selected tower prefab, hides the store UI, and enables the game's time scale. 
     * If the player has enough funds, it constructs the tower and deducts the cost from the player's balance.
     * */
    public void SelectTowerToBuild(GameObject towerPrefab, int cost)
    {
        TowerBuildPoint towerBuildPointScript = raycast.GetTowerBuildPointScript();
        ColliderEnabled();
        storeUI.SetActive(false);
        Time.timeScale = GameSpeed.isSpeedOn ? 2f : 1f;
        towerBuilder.SetTowerToBuild(towerPrefab);

        if (towerBuilder.Construct(towerBuildPointScript))
        {
            towerBuildPointScript.ChangeBuildingPermission(false);
            bank.FundsWithdrawals(cost);
        }
    }

    /* Archer1TowerPurchase(), Archer2TowerPurchase(), Mage1TowerPurchase(), and Mage2TowerPurchase(): 
     * these methods are called when the player clicks on a specific tower type in the store UI. 
     * Each method checks if the player has enough funds using the HasSufficientFunds method. 
     * If the player has enough funds, it calls the SelectTowerToBuild method to construct the tower. 
     * */
    public void Archer1TowerPurchase()
    {
        if (HasSufficientFunds(smallArcherTowerCost))
        {
            SelectTowerToBuild(towerBuilder.archerTowerPrefab, smallArcherTowerCost);
        }
    }
    public void Archer2TowerPurchase()
    {
        if (HasSufficientFunds(largeTowerArcherCost))
        {
            SelectTowerToBuild(towerBuilder.archer2TowerPrefab, largeTowerArcherCost);
        }
    }
    public void Mage1TowerPurchase()
    {
        if (HasSufficientFunds(smallMageTowerCost))
        {
            SelectTowerToBuild(towerBuilder.mageTowerPrefab, smallMageTowerCost);
        }
    }
    public void Mage2TowerPurchase()
    {
        if (HasSufficientFunds(largeMageTowerCost))
        {
            SelectTowerToBuild(towerBuilder.mage2TowerPrefab, largeMageTowerCost);
        }
    }

    // checks if the player has enough funds to purchase a tower. 
    // If the player has enough funds, it returns true.
    // If the player doesn't have enough funds, it starts a coroutine to display a "no money" message and returns false.
    private bool HasSufficientFunds(int cost)
    {
        if (bank.CurrentBalance >= cost)
        {
            return true;
        }
        else
        {
            StartCoroutine(NoMoney());
            return false;
        }
    }

    IEnumerator NoMoney()
    {
        noMoneyText.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        noMoneyText.SetActive(false);
    }

    public void ColliderEnabled()
    {
        foreach(BoxCollider collider in buildPointColliders)
        {
            collider.enabled = true;
        }
    }
}
