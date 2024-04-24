using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] float speed = 10f;

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
        float distancePerFrame = speed * Time.deltaTime;
        transform.Translate(direction.normalized * distancePerFrame, Space.World);
        /*if (direction.magnitude <= distancePerFrame)
        {
            HitTarget();
            return;
        } 
        - this block may be deleted later*/


    }

}
