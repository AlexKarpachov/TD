using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuildPoint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject constructionMarker;
    [SerializeField] GameObject storeUI;

    TowerSell towerSell;

    [SerializeField] bool canBuild = true;
    public bool CanBuild { get { return canBuild; } }

    private void Start()
    {
        towerSell = GetComponent<TowerSell>();
    }
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
