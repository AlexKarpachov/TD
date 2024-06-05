using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 10f;
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

    private void MoveToEnemy()
    {
        Vector3 direction = target.position - transform.position;
        float distancePerFrame = arrowSpeed * Time.deltaTime;
        transform.Translate(direction.normalized * distancePerFrame, Space.World);
    }
}
