using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] GameObject tower;

    /*
    The Construct method checks if a tower can be built at a specific location, and if so, it creates a new instance of the tower at that location. 
     we'll be able to know if we have enough money to build; if it's time to build
    */
    public bool Construct(BuildPoint point)
    {
        if (!point.CanBuild) return false;

        Instantiate(tower, point.transform);
        return true;
    }
}
