using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    
    // a property of the "canBuild" variable that makes info public, but all changes are possible to make only from this class (private set)
    public bool CanBuild => canBuild;

   [SerializeField] GameObject render;

    bool canBuild = true; // a boolean that tells whether we can build or not (true/false)

    // here we check whether the user can build a tower. Check for amount of money, location
    public void BuildingPermission(bool userCanBuild)
    {
        canBuild = userCanBuild;
        render.SetActive(userCanBuild);
    }
}
