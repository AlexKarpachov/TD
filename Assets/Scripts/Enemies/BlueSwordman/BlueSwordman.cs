using UnityEngine;

public class BlueSwordman : MonoBehaviour
{
    BlueSwordmanPool objectPool;
    public float EnterTime { get; set; }

    public void Initialize(BlueSwordmanPool pool)
    {
        objectPool = pool;
    }

    public void Die()
    {
        objectPool.ReturnObject(gameObject);
    }
}
