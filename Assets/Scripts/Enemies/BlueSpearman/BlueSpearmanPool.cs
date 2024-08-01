using System.Collections.Generic;
using UnityEngine;

public class BlueSpearmanPool : MonoBehaviour
{
    [SerializeField] GameObject blueSpearmanPrefab;
    [SerializeField] int poolSize = 10;

    Queue<GameObject> poolQueue;

    void Awake()
    {
        poolQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject blueSpearman = Instantiate(blueSpearmanPrefab);
            blueSpearman.SetActive(false);
            poolQueue.Enqueue(blueSpearman);
        }
    }

    // Retrieves a red spearman game object from the pool.
    // If the pool is empty, a new instance is created.
    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject blueSpearman = poolQueue.Dequeue();
            blueSpearman.SetActive(true);
            return blueSpearman;
        }
        else
        {
            GameObject blueSpearman = Instantiate(blueSpearmanPrefab);
            return blueSpearman;
        }
    }

    // Returns a red spearman game object to the pool.
    // Resets the game object's health and mover components.
    public void ReturnObject(GameObject blueSpearman)
    {
        blueSpearman.SetActive(false);
        BlueEnemyHealth healthComponent = blueSpearman.GetComponent<BlueEnemyHealth>();
        BlueEnemyMover moverComponent = blueSpearman.GetComponent<BlueEnemyMover>();
        if (healthComponent != null)
        {
            healthComponent.ResetScale();
        }

        if (moverComponent != null)
        {
            moverComponent.ResetMover();
        }
        poolQueue.Enqueue(blueSpearman);
    }
}
