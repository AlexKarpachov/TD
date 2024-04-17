using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] GameObject tower;

    // the method tells whether we succeed in building or not.
    //This script knows nothing about our building points. That's why we give the point where we want to build (BuildPoint).
    // we'll be able to know if we have enough money to build; if it's time to build
    public bool Construct(BuildPoint point)
    {
        if (!point.CanBuild) return false;

        Instantiate(tower, point.transform);
        return true;
    }
}
