using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] GameObject archerTowerPrefab;
    [SerializeField] GameObject archer2TowerPrefab;
    [SerializeField] GameObject mageTowerPrefab;
    [SerializeField] GameObject mage2TowerPrefab;

    /*
    The Construct method checks if a tower can be built at a specific location, and if so, it creates a new instance of the tower at that location. 
     we'll be able to know if we have enough money to build; if it's time to build
    */

    public bool Construct(TowerBuildPoint point)
    {
        if (!point.CanBuild) return false;

        Instantiate(archerTowerPrefab, point.transform);
        return true;
    }
}
