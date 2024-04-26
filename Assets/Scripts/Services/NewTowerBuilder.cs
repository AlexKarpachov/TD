using UnityEngine;

public class NewTowerBuilder : MonoBehaviour
{
    public GameObject archerTowerPrefab;
    public GameObject archer2TowerPrefab;
    public GameObject mageTowerPrefab;
    public GameObject mage2TowerPrefab;

    GameObject towerToBuild;
    BuildPoint buildPoint;
    public static NewTowerBuilder instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one NewTOwerBuilder in scene"); // this IF check may be deleted later
        }
        instance = this;
    }

    private void Start()
    {
    }
    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }

    public void SetTowerToBuild(GameObject tower)
    {
        towerToBuild = tower;
    }

    public void SetBuildPoint(BuildPoint point)
    {
        buildPoint = point;
        if (buildPoint != null && buildPoint.IsPlaceable)
        {
            Vector3 buildPointPosition = buildPoint.BuildPointTransform.position;
            Instantiate(towerToBuild, buildPointPosition, Quaternion.identity);
            buildPoint.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No build point assigned to NewTowerBuilder");
        }
    }
}
