using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTowerBuilder : MonoBehaviour
{
    [SerializeField] GameObject towerChoosingMenue;
    [SerializeField] GameObject archerTowerPrefab;
    GameObject towerToBuild;

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
        towerToBuild = archerTowerPrefab;
    }

    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }

    private void OnMouseDown()
    {
        towerChoosingMenue.SetActive(true);
        Time.timeScale = 0f;
    }

}
