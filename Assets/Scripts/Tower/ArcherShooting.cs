using UnityEngine;

// responsible for making the arrow seek out the target.
public class ArcherShooting : MonoBehaviour
{
    // The rate at which the archer tower fires arrows.
    [SerializeField] float fireRate = 1f; // 1f = shoots every sec; 0.5f = makes 1 shot per 2 sec.
    // The maximum distance from the archer tower that an enemy can be and still be targeted.
    [SerializeField] float towerShootingRange = 3f;

    // These variables store the current target for each type of enemy.
    [SerializeField] Transform redEnemyTarget;
    [SerializeField] Transform redSwordmanTarget;
    [SerializeField] Transform blueEnemyTarget;
    [SerializeField] Transform blueSwordmanTarget;

    // These are the transforms of the two archers at the tower.
    [SerializeField] Transform archerAtTheTower;
    [SerializeField] Transform archer2AtTheTower;

    [SerializeField] GameObject arrowPrefab;

    // The points from which the arrows will be shot.
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePoint2Archer;
    [SerializeField] float archerRotationSpeed = 10f;

    float fireCountdown = 0f;
    float time;
    float repeating;

    // These variables are used to control the frequency at which the script searches for new targets.
    private void Awake()
    {
        time = 0f; repeating = 0.5f;
    }

    void Start()
    {
        InvokeRepeating("FindClosestRedEnemy", time, repeating);
        InvokeRepeating("FindClosestBlueEnemy", time, repeating);
        InvokeRepeating("FindClosestRedSwordman", time, repeating);
        InvokeRepeating("FindClosestBlueSwordman", time, repeating);
    }

    // checks if there is a target and if the fire countdown has reached zero.
    // If both conditions are true, it calls the ShootArrow() and ShootArrow2Archer() methods.
    private void Update()
    {
        if (redEnemyTarget == null && blueEnemyTarget == null && redSwordmanTarget == null && blueSwordmanTarget == null) { return; }
        ArcherRotation();

        if (fireCountdown <= 0)
        {
            Invoke("ShootArrow", 0.3f);
            Invoke("ShootArrow2Archer", 0.3f);
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    /* FindClosestRedEnemy(), FindClosestRedSwordman(), FindClosestBlueEnemy(), and FindClosestBlueSwordman() 
     * methods search for the closest enemy of each type within the towerShootingRange and update the corresponding target variable.
     * Each of these methods uses a similar approach:
        - It finds all game objects with a specific tag (e.g., "red enemy").
        - It iterates over these game objects, calculating the distance from the archer tower to each object.
        - It keeps track of the closest object and updates the target variable if a closer object is found.
     */
    void FindClosestRedEnemy()
    {
        GameObject[] redEnemies = GameObject.FindGameObjectsWithTag("red enemy");
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in redEnemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < closestDistance)
            {
                closestTarget = enemy.transform;
                closestDistance = targetDistance;
            }
        }

        if (closestTarget != null && closestDistance <= towerShootingRange)
        {
            redEnemyTarget = closestTarget;
            redEnemyTarget.GetComponent<RedSpearman>().EnterTime = Time.time;
        }
        else
        {
            redEnemyTarget = null;
        }
    }

    void FindClosestRedSwordman()
    {
        GameObject[] redSwordmen = GameObject.FindGameObjectsWithTag("RedSwordman");
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in redSwordmen)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < closestDistance)
            {
                closestTarget = enemy.transform;
                closestDistance = targetDistance;
            }
        }

        if (closestTarget != null && closestDistance <= towerShootingRange)
        {
            redSwordmanTarget = closestTarget;
            redSwordmanTarget.GetComponent<RedSwordman>().EnterTime = Time.time;
        }
        else
        {
            redSwordmanTarget = null;
        }
    }

    void FindClosestBlueEnemy()
    {
        GameObject[] blueEnemies = GameObject.FindGameObjectsWithTag("blue enemy");
        Transform closestBlueTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject blueEnemy in blueEnemies)
        {
            float targetDistance = Vector3.Distance(transform.position, blueEnemy.transform.position);

            if (targetDistance < closestDistance)
            {
                closestBlueTarget = blueEnemy.transform;
                closestDistance = targetDistance;
            }
        }

        if (closestBlueTarget != null && closestDistance <= towerShootingRange)
        {
            blueEnemyTarget = closestBlueTarget;
            blueEnemyTarget.GetComponent<BlueEnemy>().EnterTime = Time.time;
        }
        else
        {
            blueEnemyTarget = null;
        }
    }
    void FindClosestBlueSwordman()
    {
        GameObject[] blueSwordmen = GameObject.FindGameObjectsWithTag("BlueSwordman");
        Transform closestBlueTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject blueSwordman in blueSwordmen)
        {
            float targetDistance = Vector3.Distance(transform.position, blueSwordman.transform.position);

            if (targetDistance < closestDistance)
            {
                closestBlueTarget = blueSwordman.transform;
                closestDistance = targetDistance;
            }
        }

        if (closestBlueTarget != null && closestDistance <= towerShootingRange)
        {
            blueSwordmanTarget = closestBlueTarget;
            blueSwordmanTarget.GetComponent<BlueSwordman>().EnterTime = Time.time;
        }
        else
        {
            blueSwordmanTarget = null;
        }
    }

    // The ArcherRotation() method is called to rotate the archer tower to face the closest target.
    // This method uses the FindClosestTarget() method to determine the closest target
    // and then calls the LookAtTarget() and LookAtTarget2Archer() methods to rotate the archer tower.
    private void ArcherRotation()
    {
        Transform closestTarget = FindClosestTarget();

        if (closestTarget != null)
        {
            LookAtTarget(closestTarget);
            LookAtTarget2Archer(closestTarget);
        }
    }

    private Transform FindClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;

        if (redEnemyTarget != null)
        {
            float distance = Vector3.Distance(transform.position, redEnemyTarget.position);
            if (distance < closestDistance)
            {
                closestTarget = redEnemyTarget;
                closestDistance = distance;
            }
        }

        if (blueEnemyTarget != null)
        {
            float distance = Vector3.Distance(transform.position, blueEnemyTarget.position);
            if (distance < closestDistance)
            {
                closestTarget = blueEnemyTarget;
                closestDistance = distance;
            }
        }

        if (redSwordmanTarget != null)
        {
            float distance = Vector3.Distance(transform.position, redSwordmanTarget.position);
            if (distance < closestDistance)
            {
                closestTarget = redSwordmanTarget;
                closestDistance = distance;
            }
        }

        if (blueSwordmanTarget != null)
        {
            float distance = Vector3.Distance(transform.position, blueSwordmanTarget.position);
            if (distance < closestDistance)
            {
                closestTarget = blueSwordmanTarget;
                closestDistance = distance;
            }
        }

        return closestTarget;
    }

    // The LookAtTarget() and LookAtTarget2Archer() methods use the Quaternion.LookRotation() method
    // to calculate the rotation needed to face the target.
    // They then use Quaternion.Lerp() to smoothly rotate the archer tower over time.
    void LookAtTarget(Transform target)
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(archerAtTheTower.rotation, lookRotation, Time.deltaTime * archerRotationSpeed).eulerAngles;
        archerAtTheTower.rotation = Quaternion.Euler(rotation);
    }

    void LookAtTarget2Archer(Transform target)
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(archer2AtTheTower.rotation, lookRotation, Time.deltaTime * archerRotationSpeed).eulerAngles;
        archer2AtTheTower.rotation = Quaternion.Euler(rotation);
    }

    /*The ShootArrow() and ShootArrow2Archer() methods are responsible for shooting arrows at the target.
     * They use an ArrowsPool to manage the arrows and ensure that arrows are reused instead of created and destroyed repeatedly.
     * These methods:
        - Get an arrow from the pool.
        - Initialize the arrow's script components.
        - Set the arrow's position and rotation to match the fire point.
        - Activate the arrow.
    */

    void ShootArrow()
    {
        ArrowsPool arrowsPool = FindObjectOfType<ArrowsPool>();
        GameObject tempArrow = arrowsPool.GetObject();
        ArrowShooting shootingScript = tempArrow.GetComponent<ArrowShooting>();
        if (shootingScript != null)
        {
            shootingScript.Initialize(arrowsPool);
            shootingScript.SeekEnemy(FindClosestTarget());
            tempArrow.transform.position = firePoint.position;
            tempArrow.transform.rotation = firePoint.rotation;
            tempArrow.SetActive(true);
        }
    }

    void ShootArrow2Archer()
    {
        ArrowsPool arrowsPool = FindObjectOfType<ArrowsPool>();
        GameObject tempArrow = arrowsPool.GetObject();
        ArrowShooting shootingScript = tempArrow.GetComponent<ArrowShooting>();
        if (shootingScript != null)
        {
            shootingScript.Initialize(arrowsPool);
            shootingScript.SeekEnemy(FindClosestTarget());
            tempArrow.transform.position = firePoint2Archer.position;
            tempArrow.transform.rotation = firePoint2Archer.rotation;
            tempArrow.SetActive(true);
        }
    }

    /* private void OnDrawGizmosSelected()
     {
         Gizmos.color = Color.yellow;
         Gizmos.DrawWireSphere(transform.position, range);
     }*/
}
