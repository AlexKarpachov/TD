using UnityEngine;

public class Sphere2Shooting : MonoBehaviour
{
    [SerializeField] float sphere2Speed = 10f;
    [SerializeField] float explosionRadius = 4f;
    [SerializeField] int explosionDamage = 75;
    [SerializeField] GameObject explosionParticlesPrrefab;
    [SerializeField] RedEnemyHealth reHealth;
    [SerializeField] BlueEnemyHealth beHealth;

    EnemyMoneyCalculator moneyCalculator;
    public Transform target;

    private void Start()
    {
        moneyCalculator = FindObjectOfType<EnemyMoneyCalculator>();
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
                if (reHealth != null)
                {
                    reHealth.CurrentRedEnemyHealth -= explosionDamage;
                    reHealth.healthBar.fillAmount = (float)reHealth.CurrentRedEnemyHealth / reHealth.redEnemyHealth;
                    if (reHealth.CurrentRedEnemyHealth < 1)
                    {
                        Destroy(reHealth.gameObject);
                        moneyCalculator.MoneyDeposit();
                    }
                }
            }
            else if (collider.tag == "blue enemy")
            {
                BlueEnemyHealth beHealth = collider.GetComponent<BlueEnemyHealth>();
                if (beHealth != null)
                {
                    beHealth.CurrentBlueEnemyHealth -= explosionDamage;
                    beHealth.healthBar.fillAmount = (float) beHealth.CurrentBlueEnemyHealth / beHealth.blueEnemyHhealth;
                    if (beHealth.CurrentBlueEnemyHealth < 1)
                    {
                        Destroy(beHealth.gameObject);
                        moneyCalculator.MoneyDeposit();
                    }
                }
            }
        }
    }
}
