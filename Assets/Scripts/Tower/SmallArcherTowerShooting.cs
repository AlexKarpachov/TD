using UnityEngine;

public class SmallArcherTowerShooting : MonoBehaviour
{
    [SerializeField] float fireRate = 1f;
    [SerializeField] float range = 3f;
    [SerializeField] Transform redEnemyTarget;
    [SerializeField] Transform blueEnemyTarget;
    [SerializeField] Transform archerAtTheTower;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float archerRotationSpeed = 10f;
    [SerializeField] float archerTilt = -50;

    float redEnemyEnterTime = 0f;
    float blueEnemyEnterTime = 0f;
    float fireCountdown = 0f;

    void Start()
    {
        InvokeRepeating("FindClosestRedEnemy", 0f, 0.5f);
        InvokeRepeating("FindClosestBlueEnemy", 0f, 0.5f);
    }
    private void Update()
    {
        if (redEnemyTarget == null && blueEnemyTarget == null) { return; }
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
        Transform closestTarget = null; // GameObject closestTarget = null; ???
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
            redEnemyTarget = closestTarget.transform;
            redEnemyEnterTime = Time.time;
        }
        else
        {
            redEnemyTarget = null;
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
            blueEnemyTarget = closestBlueTarget.transform;
            blueEnemyEnterTime = Time.time;
        }
        else
        {
            blueEnemyTarget = null;
        }
    }

    private void ArcherRotation()
    {
        if (redEnemyTarget != null && blueEnemyTarget != null)
        {
            if (redEnemyEnterTime < blueEnemyEnterTime)
            {
                LookAtRedEnemies();
            }
            else
            {
                LookAtBlueEnemies();
            }
        }
        else if (redEnemyTarget != null)
        {
            LookAtRedEnemies();
        }
        else if (blueEnemyTarget != null)
        {
            LookAtBlueEnemies();
        }
    }

    void LookAtRedEnemies()
    {
        Vector3 archDirection = redEnemyTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(archDirection);
        float tiltAngle = CalculateTiltAngle(archDirection);
        Vector3 rotation = Quaternion.Lerp(archerAtTheTower.rotation, lookRotation, Time.deltaTime * archerRotationSpeed).eulerAngles;
        rotation.x += tiltAngle;
        archerAtTheTower.rotation = Quaternion.Euler(rotation);
    }

    void LookAtBlueEnemies()
    {
        Vector3 archDirectionBlue = blueEnemyTarget.position - transform.position;
        Quaternion lookRotationBlue = Quaternion.LookRotation(archDirectionBlue);
        float tiltAngle = CalculateTiltAngle(archDirectionBlue);
        Vector3 rotationBlue = Quaternion.Lerp(archerAtTheTower.rotation, lookRotationBlue, Time.deltaTime * archerRotationSpeed).eulerAngles;
        rotationBlue.x += tiltAngle;
        archerAtTheTower.rotation = Quaternion.Euler(rotationBlue);
    }

    float CalculateTiltAngle(Vector3 direction)
    {
        return Mathf.Clamp(Vector3.Dot(Vector3.down, direction.normalized), -1f, 1f) * archerTilt;
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        ArrowShooting shootingScript = arrow.GetComponent<ArrowShooting>();
        if (shootingScript != null)
        {
            shootingScript.SeekEnemy(redEnemyTarget != null ? redEnemyTarget : blueEnemyTarget);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
