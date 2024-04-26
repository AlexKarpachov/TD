using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    NewTowerBuilder newTowerBuilder;

    void Start()
    {
        newTowerBuilder = NewTowerBuilder.instance;
    }

    public void Archer1TowerPurchase()
    {
        Debug.Log("Arhcer1 tower is chosen");
        newTowerBuilder.SetTowerToBuild(newTowerBuilder.archerTowerPrefab);
    }
    public void Archer2TowerPurchase()
    {
        Debug.Log("Arhcer2 tower is chosen");
        newTowerBuilder.SetTowerToBuild(newTowerBuilder.archer2TowerPrefab);
    }
    public void Mage1TowerPurchase()
    {
        Debug.Log("Mage1 tower is chosen");
        newTowerBuilder.SetTowerToBuild(newTowerBuilder.mageTowerPrefab);
    }
    public void Mage2TowerPurchase()
    {
        Debug.Log("Mage2 tower is chosen");
        newTowerBuilder.SetTowerToBuild(newTowerBuilder.mage2TowerPrefab);
    }
}
