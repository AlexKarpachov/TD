using UnityEngine;

// manages the construction of towers in a game.
public class TowerBuilder : MonoBehaviour
{
    [SerializeField] Bank bank;

    // stores the tower prefab that is currently selected for construction.
    GameObject selectedTowerPrefab; 

    public GameObject archerTowerPrefab;
    public GameObject archer2TowerPrefab;
    public GameObject mageTowerPrefab;
    public GameObject mage2TowerPrefab;

    // sets the selectedTowerPrefab field to the specified tower prefab.
    // It takes a GameObject parameter, which represents the tower prefab to select.
    public void SetTowerToBuild(GameObject tower)
    {
        selectedTowerPrefab = tower;
    }

    //The Construct method checks if a tower can be built at a specific location, and if so, it creates a new instance of the tower at that location. 
    // It takes a TowerBuildPoint parameter, which represents the build point where the tower should be constructed.
    public bool Construct(TowerBuildPoint buildPoint)
    {
        if (selectedTowerPrefab == null) return false;
        if (!buildPoint.CanBuild) return false;
        Instantiate(selectedTowerPrefab, buildPoint.transform.position, buildPoint.transform.rotation);
        return true;
    }
}
