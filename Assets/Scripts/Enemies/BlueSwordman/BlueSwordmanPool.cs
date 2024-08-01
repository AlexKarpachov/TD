using System.Collections.Generic;
using UnityEngine;

public class BlueSwordmanPool : MonoBehaviour
{
    [SerializeField] GameObject blueSwordmanPrefab;
    [SerializeField] int poolSize = 10;

    Queue<GameObject> poolQueue;

    void Awake()
    {
        poolQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject blueSwordman = Instantiate(blueSwordmanPrefab);
            blueSwordman.SetActive(false);
            poolQueue.Enqueue(blueSwordman);
        }
    }

    // Retrieves a red spearman game object from the pool.
    // If the pool is empty, a new instance is created.
    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject blueSwordman = poolQueue.Dequeue();
            blueSwordman.SetActive(true);
            return blueSwordman;
        }
        else
        {
            GameObject blueSwordman = Instantiate(blueSwordmanPrefab);
            return blueSwordman;
        }
    }

    // Returns a red spearman game object to the pool.
    // Resets the game object's health and mover components.
    public void ReturnObject(GameObject blueSwordman)
    {
        blueSwordman.SetActive(false);
        BlueSwordmanHealth healthComponent = blueSwordman.GetComponent<BlueSwordmanHealth>();
        BlueSwordmanMover moverComponent = blueSwordman.GetComponent<BlueSwordmanMover>();
        if (healthComponent != null)
        {
            healthComponent.ResetScale();
        }

        if (moverComponent != null)
        {
            moverComponent.ResetMover();
        }
        poolQueue.Enqueue(blueSwordman);
    }
}
