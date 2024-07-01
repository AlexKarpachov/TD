using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RedEnemyHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50;
    [SerializeField] float slowedSpeed = 5f;
    [SerializeField] float slowingDuration = 2f;
    [SerializeField] int currentRedEnemyHealth;
    public int CurrentRedEnemyHealth
    {
        get { return currentRedEnemyHealth; }
        set
        {
            if (value < 0)
            {
                currentRedEnemyHealth = 0;
            }
            else
            {
                currentRedEnemyHealth = value;
            }
        }
    }
    public Image healthBar;
    public int redEnemyHealth = 100;
    float originalSpeed;
    private EnemySpawner enemySpawner;

    EnemyMoneyCalculator moneyCalculator;
    NavMeshAgent navMeshAgent;

    private void OnEnable()
    {
        currentRedEnemyHealth = redEnemyHealth;
    }

    void Start()
    {
        moneyCalculator = GetComponent<EnemyMoneyCalculator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            HitByArrow();
        }
        else if (other.gameObject.CompareTag("sphere1"))
        {
            HitBySmallMage();
        }
    }

    void HitByArrow()
    {
        currentRedEnemyHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentRedEnemyHealth / redEnemyHealth;
        if (currentRedEnemyHealth < 1)
        {
            Destroy(gameObject);
            moneyCalculator.MoneyDeposit();
            enemySpawner.OnEnemyDestroyed();
        }
    }

    void HitBySmallMage()
    {
        originalSpeed = navMeshAgent.speed;
        navMeshAgent.speed = slowedSpeed;
        currentRedEnemyHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentRedEnemyHealth / redEnemyHealth;
        if (currentRedEnemyHealth < 1)
        {
            Destroy(gameObject);
            moneyCalculator.MoneyDeposit();
            enemySpawner.OnEnemyDestroyed();
        }
        StartCoroutine(SlowDownEnemy());
    }

    IEnumerator SlowDownEnemy()
    {
        yield return new WaitForSeconds(slowingDuration);
        navMeshAgent.speed = originalSpeed;
    }
}
