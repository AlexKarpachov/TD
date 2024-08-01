using UnityEngine;

// controls the movement and behavior of an arrow object
// It allows the arrow to move towards a target, hit it, and then return to a pool of available arrows.
public class ArrowShooting : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 10f;
    [SerializeField] GameObject sparksVFX;

    ArrowsPool arrowsPool;

    public Transform target;
    float destroyVFXTime;

    private void Awake()
    {
        destroyVFXTime = 0.5f;
    }

    // checks if the target is null. If it is, the arrow GameObject is destroyed, and the method returns.
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        MoveToEnemy();
    }

    // sets the arrowsPool reference, which is necessary for the arrow to return to the pool after hitting its target.
    public void Initialize(ArrowsPool _arrowsPool)
    {
        arrowsPool = _arrowsPool;
    }

    // sets the target Transform, which the arrow will move towards
    public void SeekEnemy(Transform _target)
    {
        target = _target;
    }

    /* calculates the direction from the arrow's current position to the target's position. 
     * It then calculates the distance that the arrow should move towards the target in the current frame, 
     * based on the arrowSpeed and the time since the last frame (Time.deltaTime).
     * If the distance to the target is less than or equal to the distance that the arrow should move in the current frame, 
     * the HitTarget() method is called.
     */
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

    /* called when the arrow reaches its target. 
     * It instantiates the sparks visual effect at the arrow's current position and rotation, 
     * and sets it to be destroyed after the destroyVFXTime.
     * Finally, the arrow is returned to the pool of available arrows using the ReturnObject() method of the ArrowsPool object.
     */
    void HitTarget()
    {
        GameObject sparksEffect = Instantiate(sparksVFX, transform.position, transform.rotation);
        Destroy(sparksEffect, destroyVFXTime);
        arrowsPool.ReturnObject(gameObject);
    }
}
