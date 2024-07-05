using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RedSwordmanHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50;
    [SerializeField] float slowedSpeed = 3f;
    [SerializeField] float slowingDuration = 2f;
    [SerializeField] int currentRedSwordmanHealth;
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] NavMeshAgent navMeshAgent;
    public int CurrentRedSwordmanHealth
    {
        get { return currentRedSwordmanHealth; }
        set
        {
            if (value < 0)
            {
                currentRedSwordmanHealth = 0;
            }
            else
            {
                currentRedSwordmanHealth = value;
            }
        }
    }
    public Image healthBar;
    public int redSwordmanHealth = 100;
    float originalSpeed;
    private EnemySpawner enemySpawner;

    private void OnEnable()
    {
        currentRedSwordmanHealth = redSwordmanHealth;
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
        currentRedSwordmanHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentRedSwordmanHealth / redSwordmanHealth;
        if (currentRedSwordmanHealth < 1)
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
        currentRedSwordmanHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentRedSwordmanHealth / redSwordmanHealth;
        if (currentRedSwordmanHealth < 1)
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
