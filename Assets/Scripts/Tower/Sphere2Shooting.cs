using UnityEngine;

public class Sphere2Shooting : MonoBehaviour
{
    [SerializeField] float sphere2Speed = 10f;
    [SerializeField] float explosionRadius = 4f;
    [SerializeField] int explosionDamage = 60;
    [SerializeField] int swordamnExplosionDamage = 45;
    [SerializeField] GameObject explosionParticlesPrrefab;
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
      //  redEnemySpawner = FindObjectOfType<RedEnemySpawner>();
      //  blueEnemySpawner = FindObjectOfType<BlueEnemySpawner>();
        enemyChecker = FindObjectOfType<EnemyChecker>();
    }

    public void SeekEnemy(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        MoveToEnemy();
    }

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
    void HitTarget()
    {
        GameObject exposionVFX = Instantiate(explosionParticlesPrrefab, transform.position, transform.rotation);
        Destroy(exposionVFX, destroyVFXTime);
        HitSeveralEnemies();
        Destroy(gameObject);
    }

    public void HitSeveralEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("red enemy"))
            {
                RedSpearmanHealth reHealth = collider.GetComponent<RedSpearmanHealth>();
                EnemyMoneyCalculator redEnemyMC = collider.GetComponent<EnemyMoneyCalculator>();
                if (reHealth != null)
                {
                    reHealth.CurrentRedEnemyHealth -= explosionDamage;
                    reHealth.healthBar.fillAmount = (float)reHealth.CurrentRedEnemyHealth / reHealth.redEnemyHealth;
                    if (reHealth.CurrentRedEnemyHealth < 1)
                    {
                        reHealth.GetComponent<RedSpearman>().Die();
                        enemyChecker.CheckForRemainingEnemies();
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
                    beHealth.CurrentBlueEnemyHealth -= explosionDamage;
                    beHealth.healthBar.fillAmount = (float)beHealth.CurrentBlueEnemyHealth / beHealth.blueEnemyHealth;
                    if (beHealth.CurrentBlueEnemyHealth < 1)
                    {
                        beHealth.GetComponent<BlueEnemy>().Die();
                        enemyChecker.CheckForRemainingEnemies();
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
                    rsHealth.CurrentRedSwordmanHealth -= swordamnExplosionDamage;
                    rsHealth.healthBar.fillAmount = (float)rsHealth.CurrentRedSwordmanHealth / rsHealth.redSwordmanHealth;
                    if (rsHealth.CurrentRedSwordmanHealth < 1)
                    {
                        rsHealth.GetComponent<RedSwordman>().Die();
                        enemyChecker.CheckForRemainingEnemies();
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
                    rsHealth.CurrentBlueSwordmanHealth -= swordamnExplosionDamage;
                    rsHealth.healthBar.fillAmount = (float)rsHealth.CurrentBlueSwordmanHealth / rsHealth.blueSwordmanHealth;
                    if (rsHealth.CurrentBlueSwordmanHealth < 1)
                    {
                        rsHealth.GetComponent<BlueSwordman>().Die();
                        enemyChecker.CheckForRemainingEnemies();
                        bsMoneyCalculator.MoneyDeposit();
                    }
                }
            }
        }
    }
}
