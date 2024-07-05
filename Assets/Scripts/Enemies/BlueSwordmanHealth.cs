using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BlueSwordmanHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50;
    [SerializeField] float slowedSpeed = 3f;
    [SerializeField] float slowingDuration = 2f;
    [SerializeField] int currentBlueSwordmanHealth;
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] NavMeshAgent navMeshAgent;
    public int CurrentBlueSwordmanHealth
    {
        get { return currentBlueSwordmanHealth; }
        set
        {
            if (value < 0)
            {
                currentBlueSwordmanHealth = 0;
            }
            else
            {
                currentBlueSwordmanHealth = value;
            }
        }
    }
    public Image healthBar;
    public int blueSwordmanHealth = 100;
    float originalSpeed;
    private EnemySpawner enemySpawner;

    private void OnEnable()
    {
        currentBlueSwordmanHealth = blueSwordmanHealth;
    }

    void Start()
    {
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
        currentBlueSwordmanHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentBlueSwordmanHealth / blueSwordmanHealth;
        if (currentBlueSwordmanHealth < 1)
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
        currentBlueSwordmanHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentBlueSwordmanHealth / blueSwordmanHealth;
        if (currentBlueSwordmanHealth < 1)
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
