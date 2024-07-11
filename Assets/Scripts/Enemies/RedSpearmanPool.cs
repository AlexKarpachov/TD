using System.Collections.Generic;
using UnityEngine;

public class RedSpearmanPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize = 10;

    Queue<GameObject> poolQueue;
    Transform target;

    int initialHealth;

    void Awake()
    {
        poolQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            initialHealth = obj.GetComponent<RedEnemyHealth>().redEnemyHealth;
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        RedEnemyHealth healthComponent = obj.GetComponent<RedEnemyHealth>();
        RedEnemyMover moverComponent = obj.GetComponent<RedEnemyMover>();
        if (healthComponent != null)
        {
            healthComponent.ResetScale();
        }

        if (moverComponent != null)
        {
            moverComponent.ResetMover();
        }
        poolQueue.Enqueue(obj);
    }
}
