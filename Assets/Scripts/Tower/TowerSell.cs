using UnityEngine;

public class TowerSell : MonoBehaviour
{
    [SerializeField] GameObject sellUI;
    
    [SerializeField] Bank bank;
    [SerializeField] Store store;
    // [SerializeField] Raycast raycast;
    GameObject towerPrefabOnPoint;
    TowerBuildPoint towerBuildPoint;

    int moneyToReturn;

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
        if (other.gameObject.CompareTag("SmallArcherTower"))
        {
            moneyToReturn = store.SmallArcherTowerCost / 2;
        }
        else if (other.gameObject.CompareTag("LargeArcherTower"))
        {
            moneyToReturn = store.LargeArcherTowerCost / 2;
        }
        else if (other.gameObject.CompareTag("SmallMageTower"))
        {
            moneyToReturn = store.SmallMageTowerCost / 2;
        }
        else if (other.gameObject.CompareTag("LargeMageTower"))
        {
            moneyToReturn = store.LargeMageTowerCost / 2;
        }
    }

    public void SellMethod()
    {
        bank.Deposit(moneyToReturn);
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
