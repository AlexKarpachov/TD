using UnityEngine;

// controls the movement and behavior of a blue sphere object (from small Mage tower) 
// It allows the sphere to move towards a target and hit it.
public class Sphere1Shooting : MonoBehaviour
{
    [SerializeField] float sphere1Speed = 10f;
    [SerializeField] GameObject blueExplosionPrefab;

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

    /* calculates the direction from the sphere's current position to the target's position. 
     * It then calculates the distance that the sphere should move towards the target in the current frame, 
     * based on the sphere1Speed and the time since the last frame (Time.deltaTime).
     * If the distance to the target is less than or equal to the distance that the sphere should move in the current frame, 
     * the HitTarget() method is called.
     */
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

    /* called when the sphere reaches its target. 
     * It instantiates the sparks visual effect at the sphere's current position and rotation, 
     * and sets it to be destroyed after the destroyVFXTime.
     * Finally, the sphere is destroyed.
     */
    void HitTarget()
    {
        GameObject exposionVFX = Instantiate(blueExplosionPrefab, transform.position, transform.rotation);
        Destroy(exposionVFX, destroyVFXTime);
        Destroy(gameObject);
    }
}
