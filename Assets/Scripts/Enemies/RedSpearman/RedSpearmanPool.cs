using System.Collections.Generic;
using UnityEngine;

public class RedSpearmanPool : MonoBehaviour
{
    [SerializeField] GameObject redSpearmanPrefab;
    [SerializeField] int poolSize = 10;

    Queue<GameObject> redSpearmanQueue;

    void Awake()
    {
        redSpearmanQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject redSpearman = Instantiate(redSpearmanPrefab);
            redSpearman.SetActive(false);
            redSpearmanQueue.Enqueue(redSpearman);
        }
    }

        // Retrieves a red spearman game object from the pool.
        // If the pool is empty, a new instance is created.
    public GameObject GetSpearman()
    {
        if (redSpearmanQueue.Count > 0)
        {
            GameObject redSpearman = redSpearmanQueue.Dequeue();
            redSpearman.SetActive(true);
            return redSpearman;
        }
        else
        {
            GameObject spearman = Instantiate(redSpearmanPrefab);
            return spearman;
        }
    }

    // Returns a red spearman game object to the pool.
    // Resets the game object's health and mover components.
    public void ReturnObject(GameObject redSpearman)
    {
        redSpearman.SetActive(false);
        RedSpearmanHealth healthComponent = redSpearman.GetComponent<RedSpearmanHealth>();
        RedEnemyMover moverComponent = redSpearman.GetComponent<RedEnemyMover>();
        if (healthComponent != null)
        {
            healthComponent.ResetScale();
        }

        if (moverComponent != null)
        {
            moverComponent.ResetMover();
        }
        redSpearmanQueue.Enqueue(redSpearman);
    }
}
