using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuildPoint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject constructionMarker;
    [SerializeField] GameObject storeUI;
    [SerializeField] TowerSell towerSell;
    [SerializeField] bool canBuild = true;
    public bool CanBuild { get { return canBuild; } }

    public void ChangeBuildingPermission(bool userCanBuild)
    {
        canBuild = userCanBuild;
        constructionMarker.SetActive(userCanBuild);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canBuild)
        {
            storeUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!canBuild)
        {
            towerSell.SellUIInitiation();
        }
    }
}
