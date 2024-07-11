using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointRed : MonoBehaviour
{
    public static Transform[] routeRed;

    private void Awake()
    {
        routeRed = new Transform[transform.childCount];

        for (int i = 0; i < routeRed.Length; i++)
        {
            routeRed[i] = transform.GetChild(i);
        }
    }
}
