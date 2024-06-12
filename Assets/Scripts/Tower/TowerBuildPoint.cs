using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuildPoint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject constructionMarker;
    [SerializeField] GameObject storeUI;

    bool canBuild = true;
    public bool CanBuild { get { return canBuild; } }

    /*private void OnMouseDown()
    {
        if (canBuild)
        {
            Time.timeScale = 0f;
            storeUI.SetActive(true);
        }
    }*/
    public void ChangeBuildingPermission(bool userCanBuild)
    {
        canBuild = userCanBuild;
        constructionMarker.SetActive(userCanBuild);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canBuild)
        {
            Time.timeScale = 0f;
            storeUI.SetActive(true);
        }
    }
}
