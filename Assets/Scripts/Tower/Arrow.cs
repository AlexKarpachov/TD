using UnityEngine;

public class Arrow : MonoBehaviour
{
    ArrowsPool arrowsPool;

    public void Initialize(ArrowsPool pool)
    {
        arrowsPool = pool;
    }

    public void DestroyArrow()
    {
        arrowsPool.ReturnObject(gameObject);
    }
}
