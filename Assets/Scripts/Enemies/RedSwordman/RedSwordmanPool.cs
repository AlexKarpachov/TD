using System.Collections.Generic;
using UnityEngine;

public class RedSwordmanPool : MonoBehaviour
{
    [SerializeField] GameObject redSwordmanPrefab;
    [SerializeField] int poolSize = 10;

    Queue<GameObject> poolQueue;

    void Awake()
    {
        poolQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject redSwordman = Instantiate(redSwordmanPrefab);
            redSwordman.SetActive(false);
            poolQueue.Enqueue(redSwordman);
        }
    }

    // Retrieves a red spearman game object from the pool.
    // If the pool is empty, a new instance is created.
    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject redSwordman = poolQueue.Dequeue();
            redSwordman.SetActive(true);
            return redSwordman;
        }
        else
        {
            GameObject redSwordman = Instantiate(redSwordmanPrefab);
            return redSwordman;
        }
    }

    // Returns a red spearman game object to the pool.
    // Resets the game object's health and mover components.
    public void ReturnObject(GameObject redSwordman)
    {
        redSwordman.SetActive(false);
        RedSwordmanHealth healthComponent = redSwordman.GetComponent<RedSwordmanHealth>();
        RedSwordmanMover moverComponent = redSwordman.GetComponent<RedSwordmanMover>();
        if (healthComponent != null)
        {
            healthComponent.ResetScale();
        }

        if (moverComponent != null)
        {
            moverComponent.ResetMover();
        }
        poolQueue.Enqueue(redSwordman);
    }
}
