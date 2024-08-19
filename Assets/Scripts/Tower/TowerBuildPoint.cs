using UnityEngine;
using UnityEngine.EventSystems;

// the script is responsible for managing the building of towers at specific points in the game world. 
public class TowerBuildPoint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject constructionMarker; // the unbuilt tower on the map
    [SerializeField] GameObject storeUI;
    [SerializeField] TowerSell towerSell; // A reference to a TowerSell script, which is responsible for handling the sale of towers.
    
    [SerializeField] bool canBuild = true;
    public bool CanBuild { get { return canBuild; } }

    BoxCollider[] boxColliders;

    // In start we collect all BuildPoint box colliders to disable them later.
    // We do so to exclude the possibility when the raycast goes through the Store UI to the BuildPoint, different from the chosen one.
    private void Start()
    {
        GameObject[] buildPoints = GameObject.FindGameObjectsWithTag("BuildPoint");
        boxColliders = new BoxCollider[buildPoints.Length];

        for (int i = 0; i < buildPoints.Length; i++)
        {
            boxColliders[i] = buildPoints[i].GetComponent<BoxCollider>();
        }
;    }

    // sets the value of the canBuild variable and activates or deactivates the constructionMarker GameObject based on the new value.
    public void ChangeBuildingPermission(bool userCanBuild)
    {
        canBuild = userCanBuild;
        constructionMarker.SetActive(userCanBuild);
    }

    /* is called when the player clicks on the GameObject that this script is attached to. 
     * If canBuild is true, it activates the storeUI GameObject and sets the game's time scale to 0, 
     * effectively pausing the game. If canBuild is false, it calls the SellUIInitiation() method on the towerSell script
     */

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canBuild)
        {
            ColliderDisabled();
            storeUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!canBuild)
        {
            towerSell.SellUIInitiation();
        }
    }

    void ColliderDisabled()
    {
        foreach (BoxCollider boxCollider in boxColliders)
        {
            boxCollider.enabled = false;
        }
    }
}
