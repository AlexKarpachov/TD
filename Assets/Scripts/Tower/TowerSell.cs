using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerSell : MonoBehaviour
{
    [SerializeField] GameObject sellUI;
    [SerializeField] Bank bank;
    [SerializeField] Store store;
   // [SerializeField] Raycast raycast;
    GameObject towerPrefabOnPoint;
    TowerBuildPoint towerBuildPoint;

    private void Start()
    {
        towerBuildPoint = GetComponent<TowerBuildPoint>();
    }

    public void SellUIInitiation()
    {
        if (sellUI != null)
        {
            sellUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        towerPrefabOnPoint = other.gameObject;
    }

    public void SellMethod()
    {
        bank.Deposit(store.SmallArcherTowerCost);
       // towerBuildPoint = raycast.GetTowerBuildPointScript();
        towerBuildPoint.ChangeBuildingPermission(true);
        Destroy(towerPrefabOnPoint.gameObject);
    }
    public void CloseSellMenu()
    {
        Time.timeScale = 1f;
        sellUI.SetActive(false);
    }

}
