using UnityEngine;

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
    [SerializeField] float archerTilt = -50;

    float fireCountdown = 0f;
    float time;
    float repeating;
    float Dot;

    private void Awake()
    {
        time = 0f; repeating = 0.5f;
        Dot = 1f;
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
            redEnemyTarget.GetComponent<RedEnemy>().EnterTime = Time.time;
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
            redSwordmanTarget.GetComponent<RedEnemy>().EnterTime = Time.time;
        }
        else
        {
            redSwordmanTarget = null;
        }
    }

    void FindClosestBlueEnemy()
    {
        GameObject[] blueEnemies = GameObject.FindGameObjectsWithTag("blue enemy");
        Transform closestBlueTarget = null; // GameObject closestTarget = null; ???
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
            blueSwordmanTarget.GetComponent<BlueEnemy>().EnterTime = Time.time;
        }
        else
        {
            blueSwordmanTarget = null;
        }
    }

    private void ArcherRotation()
    {
        Transform primaryTarget = null;
        if (redEnemyTarget != null)
        {
            primaryTarget = redEnemyTarget;
        }

        if (blueEnemyTarget != null && (primaryTarget == null || blueEnemyTarget.GetComponent<BlueEnemy>().EnterTime < primaryTarget.GetComponent<BlueEnemy>().EnterTime))
        {
            primaryTarget = blueEnemyTarget;
        }

        if (redSwordmanTarget != null && (primaryTarget == null || redSwordmanTarget.GetComponent<RedEnemy>().EnterTime < primaryTarget.GetComponent<RedEnemy>().EnterTime))
        {
            primaryTarget = redSwordmanTarget;
        }

        if (blueSwordmanTarget != null && (primaryTarget == null || blueSwordmanTarget.GetComponent<BlueEnemy>().EnterTime < primaryTarget.GetComponent<BlueEnemy>().EnterTime))
        {
            primaryTarget = blueSwordmanTarget;
        }

        if (primaryTarget != null)
        {
            LookAtTarget(primaryTarget);
        }
    }
    void LookAtTarget(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        float tiltAngle = CalculateTiltAngle(direction);
        Vector3 rotation = Quaternion.Lerp(archerAtTheTower.rotation, lookRotation, Time.deltaTime * archerRotationSpeed).eulerAngles;
        rotation.x += tiltAngle;
        archerAtTheTower.rotation = Quaternion.Euler(rotation);
    }

    float CalculateTiltAngle(Vector3 direction)
    {
        return Mathf.Clamp(Vector3.Dot(Vector3.down, direction.normalized), -Dot, Dot) * archerTilt;
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        ArrowShooting shootingScript = arrow.GetComponent<ArrowShooting>();
        if (shootingScript != null)
        {
            Transform target = redEnemyTarget ?? redSwordmanTarget ?? blueEnemyTarget ?? blueSwordmanTarget;
            shootingScript.SeekEnemy(target);
        }
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }*/
}
