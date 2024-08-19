using UnityEngine;

public class RedSwordman : MonoBehaviour
{
    RedSwordmanPool objectPool;

    public float EnterTime { get; set; }

    public void InitializeSwordman(RedSwordmanPool pool)
    {
        objectPool = pool;
    }

    public void Die()
    {
        objectPool.ReturnObject(gameObject);
    }
}
