using UnityEngine;

// responsible for making the mage sphere seek out the target. The description can be found in the ArcherShooting script
public class MageShootingLVL2 : MonoBehaviour
{
    [SerializeField] float fireRate = 0.5f; // 1f = shoots every sec; 0.5f = makes 1 shot per 2 sec.
    [SerializeField] float range = 3f;
    [SerializeField] Transform redEnemyTarget;
    [SerializeField] Transform redSwordmanTarget;
    [SerializeField] Transform blueEnemyTarget;
    [SerializeField] Transform blueSwordmanTarget;
    [SerializeField] GameObject sphere2Prefab;
    [SerializeField] Transform firePoint;

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

        if (fireCountdown <= 0)
        {
            ShootSphere2();
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

    void ShootSphere2()
    {
        GameObject sphere2 = Instantiate(sphere2Prefab, firePoint.position, firePoint.rotation);
        Sphere2Shooting shootingScript = sphere2.GetComponent<Sphere2Shooting>();
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
