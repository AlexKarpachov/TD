using TMPro;
using UnityEngine;

// the script manages the selling of towers, including calculating the sell price, displaying the sell UI, and handling the sale transaction.
public class TowerSell : MonoBehaviour
{
    [SerializeField] GameObject sellUI;
    [SerializeField] TextMeshProUGUI priceText; // A TextMeshProUGUI component displaying the sell price.
    [SerializeField] Bank bank; // A reference to the Bank script, which manages the player's funds.
    [SerializeField] Store store; // A reference to the Store script, which contains the costs of different towers.
    [SerializeField] TowerBuildPoint towerBuildPoint;

    GameObject towerPrefabOnPoint; // the prefab of the tower that was previously built on the build point.

    int moneyToReturn; // amount of money required to add to the player's account after the tower is been sold
    int dividingValue; // devides the initial tower cost

    private void Awake()
    {
        dividingValue = 2;
    }

    public void SellUIInitiation()
    {
        if (sellUI != null)
        {
            // Sets the text of the priceText component to display the sell price, formatted as "Sell\n+$" followed by the moneyToReturn value.
            priceText.text = "Sell\n+$" + moneyToReturn;
            sellUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    /* This method is called when the GameObject with this script stays within a trigger collider. It:
        - Retrieves the GameObject that is currently within the trigger collider.
        - Checks the tag of the GameObject to determine the type of tower.
        - Calculates the sell price (moneyToReturn) based on the tower type and the dividingTime value.
     */
    private void OnTriggerStay(Collider other)
    {
        towerPrefabOnPoint = other.gameObject;
        if (other.gameObject.CompareTag("SmallArcherTower"))
        {
            moneyToReturn = store.SmallArcherTowerCost / dividingValue;
        }
        else if (other.gameObject.CompareTag("LargeArcherTower"))
        {
            moneyToReturn = store.LargeArcherTowerCost / dividingValue;
        }
        else if (other.gameObject.CompareTag("SmallMageTower"))
        {
            moneyToReturn = store.SmallMageTowerCost / dividingValue;
        }
        else if (other.gameObject.CompareTag("LargeMageTower"))
        {
            moneyToReturn = store.LargeMageTowerCost / dividingValue;
        }
    }

    /* This method is called when the player confirms the sale of a tower. It:
        - Deposits the sell price (moneyToReturn) into the player's bank using the Bank script.
        - Changes the building permission for the tower build point to true using the TowerBuildPoint script.
        - Destroys the tower GameObject.
     */
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
