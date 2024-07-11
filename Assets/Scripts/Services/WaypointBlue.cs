using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointBlue : MonoBehaviour
{
    public static Transform[] routeBlue;

    private void Awake()
    {
        routeBlue = new Transform[transform.childCount];

        for (int i = 0; i < routeBlue.Length; i++)
        {
            routeBlue[i] = transform.GetChild(i);
        }
    }
}
