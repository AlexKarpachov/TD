using System.Threading;
using UnityEngine;

public class Sphere2Shooting : MonoBehaviour
{
    [SerializeField] float sphere2Speed = 10f;
    [SerializeField] float explosionRadius = 4f;
    [SerializeField] int explosionDamage = 60;
    [SerializeField] int swordamnExplosionDamage = 45;
    [SerializeField] GameObject explosionParticlesPrrefab;
    [SerializeField] RedEnemyHealth reHealth;
    [SerializeField] BlueEnemyHealth beHealth;
    [SerializeField] BlueSwordmanHealth bsHealth;
    [SerializeField] RedSwordmanHealth rsHealth;

    public Transform target;
    private EnemySpawner enemySpawner;

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
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
        Destroy(exposionVFX, 0.5f);
        HitSeveralEnemies();
        Destroy(gameObject);
    }

    public void HitSeveralEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "red enemy")
            {
                RedEnemyHealth reHealth = collider.GetComponent<RedEnemyHealth>();
                EnemyMoneyCalculator redEnemyMC = collider.GetComponent<EnemyMoneyCalculator>();
                if (reHealth != null)
                {
                    reHealth.CurrentRedEnemyHealth -= explosionDamage;
                    reHealth.healthBar.fillAmount = (float)reHealth.CurrentRedEnemyHealth / reHealth.redEnemyHealth;
                    if (reHealth.CurrentRedEnemyHealth < 1)
                    {
                        Destroy(reHealth.gameObject);
                        enemySpawner.OnEnemyDestroyed();
                        redEnemyMC.MoneyDeposit();
                    }
                }
            }
            else if (collider.tag == "blue enemy")
            {
                BlueEnemyHealth beHealth = collider.GetComponent<BlueEnemyHealth>();
                EnemyMoneyCalculator blueEnemyMC = collider.GetComponent<EnemyMoneyCalculator>();
                if (beHealth != null)
                {
                    beHealth.CurrentBlueEnemyHealth -= explosionDamage;
                    beHealth.healthBar.fillAmount = (float) beHealth.CurrentBlueEnemyHealth / beHealth.blueEnemyHealth;
                    if (beHealth.CurrentBlueEnemyHealth < 1)
                    {
                        Destroy(beHealth.gameObject);
                        enemySpawner.OnEnemyDestroyed();
                        blueEnemyMC.MoneyDeposit();
                    }
                }
            }
            else if (collider.tag == "RedSwordman")
            {
                RedSwordmanHealth rsHealth = collider.GetComponent<RedSwordmanHealth>();
                EnemyMoneyCalculator rsMoneyCalculator = collider.GetComponent<EnemyMoneyCalculator>();
                if (rsHealth != null)
                {
                    rsHealth.CurrentRedSwordmanHealth -= swordamnExplosionDamage;
                    rsHealth.healthBar.fillAmount = (float)rsHealth.CurrentRedSwordmanHealth / rsHealth.redSwordmanHealth;
                    if (rsHealth.CurrentRedSwordmanHealth < 1)
                    {
                        Destroy(rsHealth.gameObject);
                        enemySpawner.OnEnemyDestroyed();
                        rsMoneyCalculator.MoneyDeposit();
                    }
                }
            }
            else if (collider.tag == "BlueSwordman")
            {
                BlueSwordmanHealth rsHealth = collider.GetComponent<BlueSwordmanHealth>();
                EnemyMoneyCalculator bsMoneyCalculator = collider.GetComponent<EnemyMoneyCalculator>();
                if (rsHealth != null)
                {
                    rsHealth.CurrentBlueSwordmanHealth -= swordamnExplosionDamage;
                    rsHealth.healthBar.fillAmount = (float)rsHealth.CurrentBlueSwordmanHealth / rsHealth.blueSwordmanHealth;
                    if (rsHealth.CurrentBlueSwordmanHealth < 1)
                    {
                        Destroy(rsHealth.gameObject);
                        enemySpawner.OnEnemyDestroyed();
                        bsMoneyCalculator.MoneyDeposit();
                    }
                }
            }
        }
    }
}
