using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    RedSpearmanPool objectPool;

    public float EnterTime { get; set; }

    public void Initialize(RedSpearmanPool pool)
    {
        objectPool = pool;
    }

    public void Die()
    {
        objectPool.ReturnObject(gameObject);
    }
}
