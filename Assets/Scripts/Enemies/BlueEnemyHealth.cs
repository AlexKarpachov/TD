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
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] BlueEnemyMover mover;
    [SerializeField] RedSpearmanPool enemyPool;
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
    public int blueEnemyHealth = 100;
    float originalSpeed;
    private EnemySpawner enemySpawner;

    private void OnEnable()
    {
        currentBlueEnemyHealth = blueEnemyHealth;
    }

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
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
        currentBlueEnemyHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentBlueEnemyHealth / blueEnemyHealth;
        if (currentBlueEnemyHealth < 1)
        {
            moneyCalculator.MoneyDeposit();
            enemySpawner.OnEnemyDestroyed(gameObject);
        }
    }

    void HitBySmallMage()
    {
        originalSpeed = mover.speed;
        mover.speed = slowedSpeed;
        currentBlueEnemyHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentBlueEnemyHealth / blueEnemyHealth;
        if (currentBlueEnemyHealth < 1)
        {
            moneyCalculator.MoneyDeposit();
            enemySpawner.OnEnemyDestroyed(gameObject);
        }
        else
        {
            StartCoroutine(SlowDownEnemy());
        }
    }
    IEnumerator SlowDownEnemy()
    {
        yield return new WaitForSeconds(slowingDuration);
        mover.speed = originalSpeed;
    }
    public void ResetScale()
    {
        healthBar.fillAmount = 1;

    }
}
