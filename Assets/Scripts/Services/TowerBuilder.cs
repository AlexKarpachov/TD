using System.Drawing;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    //[SerializeField] Raycast raycast;
    [SerializeField] Bank bank;

    public GameObject archerTowerPrefab;
    public GameObject archer2TowerPrefab;
    public GameObject mageTowerPrefab;
    public GameObject mage2TowerPrefab;

    GameObject selectedTowerPrefab;

    /*
    The Construct method checks if a tower can be built at a specific location, and if so, it creates a new instance of the tower at that location. 
     we'll be able to know if we have enough money to build; if it's time to build
    */

    public void SetTowerToBuild(GameObject tower)
    {
        selectedTowerPrefab = tower;
    }

    public bool Construct(TowerBuildPoint buildPoint)
    {
        if (selectedTowerPrefab == null) return false;
        if (!buildPoint.CanBuild) return false;
        Instantiate(selectedTowerPrefab, buildPoint.transform.position, buildPoint.transform.rotation);
        return true;
    }
}
