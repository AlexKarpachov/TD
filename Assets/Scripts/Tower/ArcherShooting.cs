using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArcherShooting : MonoBehaviour
{
    [SerializeField] float range = 3f;
    [SerializeField] Transform redEnemyTarget;
    [SerializeField] Transform blueEnemyTarget;
    [SerializeField] Transform archerAtTheTower;
    [SerializeField] Transform archer2AtTheTower;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform firePoint2Archer;
    [SerializeField] float archerRotationSpeed = 10f;

    float redEnemyEnterTime = 0f;
    float blueEnemyEnterTime = 0f;
    float fireRate = 1f;
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
            Invoke ("ShootArrow", 0.5f);
            Invoke("ShootArrow2Archer", 0.5f);
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
                LookAtRedEnemies2Archer();
            }
            else
            {
                LookAtBlueEnemies();
            }
        }
        else if (redEnemyTarget != null)
        {
            LookAtRedEnemies();
            LookAtRedEnemies2Archer();
        }
        else if (blueEnemyTarget != null)
        {
            LookAtBlueEnemies();
        }
    }

    void LookAtRedEnemies()
    {
        //float targetDistance = Vector3.Distance(transform.position, redEnemyTarget.position);
        Vector3 archDirection = redEnemyTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(archDirection);
        Vector3 rotation = Quaternion.Lerp(archerAtTheTower.rotation, lookRotation, Time.deltaTime * archerRotationSpeed).eulerAngles;
        archerAtTheTower.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    void LookAtRedEnemies2Archer()
    {
        //float targetDistance = Vector3.Distance(transform.position, redEnemyTarget.position);
        Vector3 archDirection = redEnemyTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(archDirection);
        Vector3 rotation = Quaternion.Lerp(archer2AtTheTower.rotation, lookRotation, Time.deltaTime * archerRotationSpeed).eulerAngles;
        archer2AtTheTower.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    void LookAtBlueEnemies()
    {
        Vector3 archDirectionBlue = blueEnemyTarget.position - transform.position;
        Quaternion lookRotationBlue = Quaternion.LookRotation(archDirectionBlue);
        Vector3 rotationBlue = Quaternion.Lerp(archerAtTheTower.rotation, lookRotationBlue, Time.deltaTime * archerRotationSpeed).eulerAngles;
        archerAtTheTower.rotation = Quaternion.Euler(0f, rotationBlue.y, 0f);
    }

    void LookAtBlueEnemies2Archer()
    {
        Vector3 archDirectionBlue = blueEnemyTarget.position - transform.position;
        Quaternion lookRotationBlue = Quaternion.LookRotation(archDirectionBlue);
        Vector3 rotationBlue = Quaternion.Lerp(archer2AtTheTower.rotation, lookRotationBlue, Time.deltaTime * archerRotationSpeed).eulerAngles;
        archer2AtTheTower.rotation = Quaternion.Euler(0f, rotationBlue.y, 0f);
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Shooting shootingScript = arrow.GetComponent<Shooting>();
        if (shootingScript != null )
        {
            shootingScript.SeekEnemy(redEnemyTarget != null ? redEnemyTarget : blueEnemyTarget);
        }
    }

    void ShootArrow2Archer()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint2Archer.position, firePoint2Archer.rotation);
        Shooting shootingScript = arrow.GetComponent<Shooting>();
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
