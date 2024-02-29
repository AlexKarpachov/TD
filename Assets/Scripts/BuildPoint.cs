using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    // a boolean that tells whether we can build or not (true/false)
    bool canBuild = true;
    // a property of the "canBuild" variable that makes info public, but all changes are possible to make only from this class (private set)
    public bool CanBuild => canBuild;
   [SerializeField] GameObject render;

    

    // here we check whether the user can build a tower. Check for amount of money, location
    public void BuildingPermission(bool userCanBuild)
    {
        canBuild = userCanBuild;
        render.SetActive(userCanBuild);
    }
}
