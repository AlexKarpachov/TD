using UnityEngine;

// responsible for making the arrow seek out the target. The description can be found in the ArcherShooting script
public class SmallArcherTowerShooting : MonoBehaviour
{
    [SerializeField] float fireRate = 1f;
    [SerializeField] float range = 3f;
    [SerializeField] Transform redEnemyTarget;
    [SerializeField] Transform redSwordmanTarget;
    [SerializeField] Transform blueEnemyTarget;
    [SerializeField] Transform blueSwordmanTarget;
    [SerializeField] Transform archerAtTheTower;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float archerRotationSpeed = 10f;

    float fireCountdown = 0f;
    float time;
    float repeating;

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
    private void Update()
    {
        if (redEnemyTarget == null && blueEnemyTarget == null && redSwordmanTarget == null && blueSwordmanTarget == null) { return; }
        ArcherRotation();

        if (fireCountdown <= 0)
        {
            Invoke("ShootArrow", 0.3f);
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

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

        if (closestTarget != null && closestDistance <= range)
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

        if (closestTarget != null && closestDistance <= range)
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

        if (closestBlueTarget != null && closestDistance <= range)
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

        if (closestBlueTarget != null && closestDistance <= range)
        {
            blueSwordmanTarget = closestBlueTarget;
            blueSwordmanTarget.GetComponent<BlueSwordman>().EnterTime = Time.time;
        }
        else
        {
            blueSwordmanTarget = null;
        }
    }

    private void ArcherRotation()
    {
        Transform closestTarget = FindClosestTarget();

        if (closestTarget != null)
        {
            LookAtTarget(closestTarget);
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

    void LookAtTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(archerAtTheTower.rotation, lookRotation, Time.deltaTime * archerRotationSpeed).eulerAngles;
        archerAtTheTower.rotation = Quaternion.Euler(rotation);
    }

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

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }*/
}
