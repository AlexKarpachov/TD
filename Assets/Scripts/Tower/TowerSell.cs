using TMPro;
using UnityEngine;

public class TowerSell : MonoBehaviour
{
    [SerializeField] GameObject sellUI;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] Bank bank;
    [SerializeField] Store store;
    [SerializeField] TowerBuildPoint towerBuildPoint;

    GameObject towerPrefabOnPoint;

    int moneyToReturn;
    int dividingTime;

    private void Awake()
    {
        dividingTime = 2;
    }

    public void SellUIInitiation()
    {
        if (sellUI != null)
        {
            priceText.text = "Sell\n+$" + moneyToReturn;
            sellUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        towerPrefabOnPoint = other.gameObject;
        if (other.gameObject.CompareTag("SmallArcherTower"))
        {
            moneyToReturn = store.SmallArcherTowerCost / dividingTime;
        }
        else if (other.gameObject.CompareTag("LargeArcherTower"))
        {
            moneyToReturn = store.LargeArcherTowerCost / dividingTime;
        }
        else if (other.gameObject.CompareTag("SmallMageTower"))
        {
            moneyToReturn = store.SmallMageTowerCost / dividingTime;
        }
        else if (other.gameObject.CompareTag("LargeMageTower"))
        {
            moneyToReturn = store.LargeMageTowerCost / dividingTime;
        }
    }

    public void SellMethod()
    {
        bank.Deposit(moneyToReturn);
        towerBuildPoint.ChangeBuildingPermission(true);
        Destroy(towerPrefabOnPoint.gameObject);
    }
    public void CloseSellMenu()
    {
        Time.timeScale = 1f;
        sellUI.SetActive(false);
    }

}
