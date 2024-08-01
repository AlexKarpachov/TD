using UnityEngine;

public class WaypointBlue : MonoBehaviour
{
    // A static array of Transforms representing the route.
    public static Transform[] routeBlue;

    // Initializes the routeBlue array with the child Transforms of this GameObject.
    private void Awake()
    {
        routeBlue = new Transform[transform.childCount];

        for (int i = 0; i < routeBlue.Length; i++)
        {
            routeBlue[i] = transform.GetChild(i);
        }
    }
}
