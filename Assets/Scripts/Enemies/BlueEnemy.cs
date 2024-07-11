using UnityEngine;

public class BlueEnemy : MonoBehaviour
{
    BlueSpearmanPool objectPool;
    public float EnterTime { get; set; }


    public void Initialize(BlueSpearmanPool pool)
    {
        objectPool = pool;
    }

    public void Die()
    {
        objectPool.ReturnObject(gameObject);
    }
}
