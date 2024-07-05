using UnityEngine;

public class RedSpearmanObjectPool : MonoBehaviour
{
    [SerializeField] GameObject redSpearmanPrefab;
    [SerializeField] int poolSize = 5;

    GameObject[] redSpearmanPool;

    void Awake()
    {
        PopulatePool();
    }

    void PopulatePool()
    {
        redSpearmanPool = new GameObject[poolSize];

        for (int i = 0; i < redSpearmanPool.Length; i++)
        {
            redSpearmanPool[i] = Instantiate(redSpearmanPrefab, transform);
            redSpearmanPool[i].SetActive(false);
        }
    }
}
