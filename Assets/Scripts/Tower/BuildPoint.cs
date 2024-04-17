using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    [SerializeField] GameObject render;

    bool canBuild = true; // a boolean that tells whether we can build or not
    // a property of the "canBuild" variable that makes info public, but all changes are possible to make only from this class (private set)
    public bool CanBuild => canBuild;

    // here we check whether the user can build a tower. Check for amount of money, location
    public void BuildingPermission(bool userCanBuild)
    {
        canBuild = userCanBuild;
        render.SetActive(userCanBuild);
    }
}
