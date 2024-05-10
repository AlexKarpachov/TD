using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] TowerBuilder towerBuilder;
    [SerializeField] GameObject storeUI;
    [SerializeField] Raycast raycast;

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
        Debug.Log("Arhcer1 tower is chosen");
        SelectTowerToBuild(towerBuilder.archerTowerPrefab);
    }
    public void Archer2TowerPurchase()
    {
        Debug.Log("Arhcer2 tower is chosen");
        SelectTowerToBuild(towerBuilder.archer2TowerPrefab);
    }
    public void Mage1TowerPurchase()
    {
        Debug.Log("Mage1 tower is chosen");
        SelectTowerToBuild(towerBuilder.mageTowerPrefab);

    }
    public void Mage2TowerPurchase()
    {
        Debug.Log("Mage2 tower is chosen");
        SelectTowerToBuild(towerBuilder.mage2TowerPrefab);

    }
}
