using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RedSpearmanHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50;
    [SerializeField] float slowedSpeed = 4f;
    [SerializeField] float slowingDuration = 2f;
    [SerializeField] int currentRedEnemyHealth;
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] RedEnemyMover mover;

    EnemyChecker enemyChecker;
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
    private RedEnemySpawner enemySpawner;

    private void OnEnable()
    {
        currentRedEnemyHealth = redEnemyHealth;
    }

    void Start()
    {
        enemySpawner = FindObjectOfType<RedEnemySpawner>();
        enemyChecker = FindObjectOfType<EnemyChecker>();
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
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<RedSpearman>().Die();
            enemyChecker.CheckForRemainingEnemies();
        }
    }

    void HitBySmallMage()
    {
        originalSpeed = mover.speed;
        mover.speed = slowedSpeed;
        currentRedEnemyHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentRedEnemyHealth / redEnemyHealth;
        if (currentRedEnemyHealth < 1)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<RedSpearman>().Die();
            enemyChecker.CheckForRemainingEnemies();
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
