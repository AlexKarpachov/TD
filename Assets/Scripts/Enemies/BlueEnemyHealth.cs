using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BlueEnemyHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50;
    [SerializeField] float slowedSpeed = 5f;
    [SerializeField] float slowingDuration = 2f;
    [SerializeField] int currentBlueEnemyHealth;
    public int CurrentBlueEnemyHealth
    {
        get { return currentBlueEnemyHealth; }
        set
        {
            if (value < 0)
            {
                currentBlueEnemyHealth = 0;
            }
            else
            {
                currentBlueEnemyHealth = value;
            }
        }
    }
    public Image healthBar;
    public int blueEnemyHhealth = 100;
    float originalSpeed;

    EnemyMoneyCalculator moneyCalculator;
    NavMeshAgent navMeshAgent;

    private void OnEnable()
    {
        currentBlueEnemyHealth = blueEnemyHhealth;
    }

    void Start()
    {
        moneyCalculator = GetComponent<EnemyMoneyCalculator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (currentBlueEnemyHealth < 1)
        {
            Destroy(gameObject);
            moneyCalculator.MoneyDeposit();
        }
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
        originalSpeed = navMeshAgent.speed;
        navMeshAgent.speed = slowedSpeed;
        currentBlueEnemyHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentBlueEnemyHealth / blueEnemyHhealth;
        if (currentBlueEnemyHealth < 1)
        {
            Destroy(gameObject);
            moneyCalculator.MoneyDeposit();
        }
    }

    void HitBySmallMage()
    {
        originalSpeed = navMeshAgent.speed;
        navMeshAgent.speed = slowedSpeed;
        currentBlueEnemyHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentBlueEnemyHealth / blueEnemyHhealth;
        if (currentBlueEnemyHealth < 1)
        {
            Destroy(gameObject);
            moneyCalculator.MoneyDeposit();
        }
        StartCoroutine(SlowDownBlueEnemy());
    }
    IEnumerator SlowDownBlueEnemy()
    {
        yield return new WaitForSeconds(slowingDuration);
        navMeshAgent.speed = originalSpeed;
    }
}
