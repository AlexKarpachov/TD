using UnityEngine;

public class Sphere1Shooting : MonoBehaviour
{
    [SerializeField] float sphere1Speed = 10f;
    [SerializeField] GameObject blueExplosionPrefab;

    public Transform target;

    public void SeekEnemy(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        MoveToEnemy();
    }

    void MoveToEnemy()
    {
        Vector3 direction = target.position - transform.position;
        float distancePerFrame = sphere1Speed * Time.deltaTime;
        if (direction.magnitude <= distancePerFrame)
        {
            HitTarget();
        }
        transform.Translate(direction.normalized * distancePerFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject exposionVFX = Instantiate(blueExplosionPrefab, transform.position, transform.rotation);
        Destroy(exposionVFX, 0.5f);
        Destroy(gameObject);
    }
}
