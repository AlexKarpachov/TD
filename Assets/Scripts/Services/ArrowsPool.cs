using System.Collections.Generic;
using UnityEngine;

public class ArrowsPool : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] int poolSize = 10;

    Queue<GameObject> poolQueue;

    void Awake()
    {
        poolQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false);
            poolQueue.Enqueue(arrow);
        }
    }

    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject arrow = poolQueue.Dequeue();
            arrow.SetActive(true);
            return arrow;
        }
        else
        {
            GameObject arrow = Instantiate(arrowPrefab);
            return arrow;
        }
    }

    public void ReturnObject(GameObject arrow)
    {
        arrow.SetActive(false);
        poolQueue.Enqueue(arrow);
    }
}
