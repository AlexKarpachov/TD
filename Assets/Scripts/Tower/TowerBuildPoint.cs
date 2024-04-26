using UnityEngine;

public class TowerBuildPoint : MonoBehaviour
{
    [SerializeField] GameObject constructionMarker;

    bool canBuild = true;
    public bool CanBuild { get { return canBuild; } }

    public void ChangeBuildingPermission(bool userCanBuild)
    {
        canBuild = userCanBuild;
        constructionMarker.SetActive(userCanBuild);
    }
}
