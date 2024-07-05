using UnityEngine;

public class ArrowShooting : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 10f;
    [SerializeField] GameObject sparksVFX;

    public Transform target;
    float destroyVFXTime;

    private void Awake()
    {
        destroyVFXTime = 0.5f;
    }

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
        float distancePerFrame = arrowSpeed * Time.deltaTime;

        if (direction.magnitude <= distancePerFrame)
        {
            HitTarget();
        }
        transform.Translate(direction.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }
    void HitTarget()
    {
        GameObject sparksEffect = Instantiate(sparksVFX, transform.position, transform.rotation);
        Destroy(sparksEffect, destroyVFXTime);
        Destroy(gameObject);
    }
}
