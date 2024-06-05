using UnityEngine;

public class MageShootingLVL1 : MonoBehaviour
{
    [SerializeField] float range = 3f;
    [SerializeField] Transform redEnemyTarget;
    [SerializeField] Transform blueEnemyTarget;
    [SerializeField] GameObject sphere1Prefab;
    [SerializeField] Transform firePoint;

    float redEnemyEnterTime = 0f;
    float blueEnemyEnterTime = 0f;
    float fireRate = 0.5f;
    float fireCountdown = 0f;

    void Start()
    {
        InvokeRepeating("FindClosestRedEnemy", 0f, 0.5f);
        InvokeRepeating("FindClosestBlueEnemy", 0f, 0.5f);
    }
    private void Update()
    {
        if (redEnemyTarget == null && blueEnemyTarget == null) { return; }

        if (fireCountdown <= 0)
        {
            ShootSphere1();
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
            blueEnemyTarget = closestBlueTarget.transform;
            blueEnemyEnterTime = Time.time;
        }
        else
        {
            blueEnemyTarget = null;
        }
    }
    
    void ShootSphere1()
    {
        GameObject sphere1 = Instantiate(sphere1Prefab, firePoint.position, firePoint.rotation);
        Sphere1Shooting shootingScript = sphere1.GetComponent<Sphere1Shooting>();
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
