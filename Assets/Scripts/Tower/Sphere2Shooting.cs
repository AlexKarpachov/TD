using UnityEngine;

// controls a sphere that moves towards a target enemy and deals damage to enemies within an explosion radius when it reaches the target. 
// It also uses references to health scripts for different enemy types to apply damage and update the enemy's health.
public class Sphere2Shooting : MonoBehaviour
{
    [SerializeField] float sphere2Speed = 10f;
    [SerializeField] float explosionRadius = 4f;
    [SerializeField] int spearmanDamage = 60;
    [SerializeField] int swordamnDamage = 45;
    [SerializeField] GameObject explosionParticlesPrrefab;

    // references to health scripts for different enemy types
    [SerializeField] RedSpearmanHealth reHealth;
    [SerializeField] BlueEnemyHealth beHealth;
    [SerializeField] BlueSwordmanHealth bsHealth;
    [SerializeField] RedSwordmanHealth rsHealth;

    RedEnemySpawner redEnemySpawner;
    BlueEnemySpawner blueEnemySpawner;
    EnemyChecker enemyChecker;

    public Transform target;
    float destroyVFXTime;

    private void Awake()
    {
        destroyVFXTime = 0.5f;
    }

    private void Start()
    {
        enemyChecker = FindObjectOfType<EnemyChecker>();
    }

    // This method sets the target variable to the provided Transform object.
    public void SeekEnemy(Transform _target)
    {
        target = _target;
    }

    // checks if the target variable is null, and if so, destroys the GameObject. Otherwise, it calls the MoveToEnemy method.
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        MoveToEnemy();
    }

    /* calculates the direction from the sphere to the target and moves the sphere towards the target at a rate of sphere2Speed per frame. 
     * If the sphere reaches the target, it calls the HitTarget method.
     */
    void MoveToEnemy()
    {
        Vector3 direction = target.position - transform.position;
        float distancePerFrame = sphere2Speed * Time.deltaTime;
        if (direction.magnitude <= distancePerFrame)
        {
            HitTarget();
        }
        transform.Translate(direction.normalized * distancePerFrame, Space.World);
    }

    // This method instantiates the explosion particles prefab at the sphere's position and rotation,
    // destroys it after destroyVFXTime seconds, calls the HitSeveralEnemies method, and destroys the sphere GameObject.
    void HitTarget()
    {
        GameObject exposionVFX = Instantiate(explosionParticlesPrrefab, transform.position, transform.rotation);
        Destroy(exposionVFX, destroyVFXTime);
        HitSeveralEnemies();
        Destroy(gameObject);
    }

    // This method uses a physics overlap sphere to detect colliders within the explosion radius
    // and applies damage to the enemies within the colliders.
    public void HitSeveralEnemies()
    {
        // This line uses the Physics.OverlapSphere() function to retrieve an array of colliders
        // that are within the specified explosionRadius from the sphere's current position.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        /*  checks the tag of each collider to determine the type of enemy it has hit.
         *  For each tag, the script performs the following actions:
            - gets the health component associated with the enemy type;
            - If the health component is found, the script applies the specified damage to the enemy's health.
            - updates the enemy's health bar to reflect the new health value.
            If the enemy's health falls below 1, the script performs additional actions:
            - calls the Die() method on the enemy script to handle death-related logic;
            - notifies the EnemyChecker script to check for remaining enemies;
            - deposits money as a reward for killing the enemy.
         */
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("red enemy"))
            {
                RedSpearmanHealth reHealth = collider.GetComponent<RedSpearmanHealth>();
                EnemyMoneyCalculator redEnemyMC = collider.GetComponent<EnemyMoneyCalculator>();
                if (reHealth != null)
                {
                    reHealth.CurrentRedEnemyHealth -= spearmanDamage;
                    reHealth.healthBar.fillAmount = (float)reHealth.CurrentRedEnemyHealth / reHealth.redEnemyHealth;
                    if (reHealth.CurrentRedEnemyHealth < 1)
                    {
                        reHealth.GetComponent<RedSpearman>().Die();
                        if (RedEnemySpawner.lastwave == true) { enemyChecker.CheckForRemainingEnemies(); }
                        redEnemyMC.MoneyDeposit();
                    }
                }
            }
            else if (collider.CompareTag("blue enemy"))
            {
                BlueEnemyHealth beHealth = collider.GetComponent<BlueEnemyHealth>();
                EnemyMoneyCalculator blueEnemyMC = collider.GetComponent<EnemyMoneyCalculator>();
                if (beHealth != null)
                {
                    beHealth.CurrentBlueEnemyHealth -= spearmanDamage;
                    beHealth.healthBar.fillAmount = (float)beHealth.CurrentBlueEnemyHealth / beHealth.blueEnemyHealth;
                    if (beHealth.CurrentBlueEnemyHealth < 1)
                    {
                        beHealth.GetComponent<BlueEnemy>().Die();
                        if (RedEnemySpawner.lastwave == true) { enemyChecker.CheckForRemainingEnemies(); }
                        blueEnemyMC.MoneyDeposit();
                    }
                }
            }
            else if (collider.CompareTag("RedSwordman"))
            {
                RedSwordmanHealth rsHealth = collider.GetComponent<RedSwordmanHealth>();
                EnemyMoneyCalculator rsMoneyCalculator = collider.GetComponent<EnemyMoneyCalculator>();
                if (rsHealth != null)
                {
                    rsHealth.CurrentRedSwordmanHealth -= swordamnDamage;
                    rsHealth.healthBar.fillAmount = (float)rsHealth.CurrentRedSwordmanHealth / rsHealth.redSwordmanHealth;
                    if (rsHealth.CurrentRedSwordmanHealth < 1)
                    {
                        rsHealth.GetComponent<RedSwordman>().Die();
                        if (RedEnemySpawner.lastwave == true) { enemyChecker.CheckForRemainingEnemies(); }
                        rsMoneyCalculator.MoneyDeposit();
                    }
                }
            }
            else if (collider.CompareTag("BlueSwordman"))
            {
                BlueSwordmanHealth rsHealth = collider.GetComponent<BlueSwordmanHealth>();
                EnemyMoneyCalculator bsMoneyCalculator = collider.GetComponent<EnemyMoneyCalculator>();
                if (rsHealth != null)
                {
                    rsHealth.CurrentBlueSwordmanHealth -= swordamnDamage;
                    rsHealth.healthBar.fillAmount = (float)rsHealth.CurrentBlueSwordmanHealth / rsHealth.blueSwordmanHealth;
                    if (rsHealth.CurrentBlueSwordmanHealth < 1)
                    {
                        rsHealth.GetComponent<BlueSwordman>().Die();
                        if (RedEnemySpawner.lastwave == true) { enemyChecker.CheckForRemainingEnemies(); }
                        bsMoneyCalculator.MoneyDeposit();
                    }
                }
            }
        }
    }
}
