using System.Collections.Generic;
using UnityEngine;

public class RedSpearmanPool : MonoBehaviour
{
    [SerializeField] GameObject spearmanPrefab;
    [SerializeField] int poolSize = 10;

    Queue<GameObject> spearmanQueue;

    void Awake()
    {
        spearmanQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject spearman = Instantiate(spearmanPrefab);
            spearman.SetActive(false);
            spearmanQueue.Enqueue(spearman);
        }
    }

    public GameObject GetObject()
    {
        if (spearmanQueue.Count > 0)
        {
            GameObject obj = spearmanQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(spearmanPrefab);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        RedSpearmanHealth healthComponent = obj.GetComponent<RedSpearmanHealth>();
        RedEnemyMover moverComponent = obj.GetComponent<RedEnemyMover>();
        if (healthComponent != null)
        {
            healthComponent.ResetScale();
        }

        if (moverComponent != null)
        {
            moverComponent.ResetMover();
        }
        spearmanQueue.Enqueue(obj);
    }
}
