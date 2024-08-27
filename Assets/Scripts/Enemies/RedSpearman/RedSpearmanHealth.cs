using UnityEngine;
using UnityEngine.UI;

public class RedSpearmanHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50; // The damage dealt by a sphere1 (Mage1 tower).
    [SerializeField] float slowedSpeed = 4f; // The slowed speed of the enemy when hit by a sphere1.
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] RedEnemyMover mover;

    EnemyChecker enemyChecker;

    int currentRedEnemyHealth;
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
    public Image healthBar; // The health bar UI image.
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
        // Called when the enemy is hit by an arrow
        // Reduce the enemy's health by 20
        // if the health is less than 0, destroys the enemy and pays $10 reward to the player
        currentRedEnemyHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentRedEnemyHealth / redEnemyHealth;
        if (currentRedEnemyHealth <= 0)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<RedSpearman>().Die();
            enemyChecker.CheckForRemainingEnemies();
        }
    }

    void HitBySmallMage()
    {
        // Called when the enemy is hit by sphere1 (Mage1)
        // Reduce the enemy's health by 30
        // if the health is less than 0, destroys the enemy and pays $10 reward to the player
        mover.redSpearmanSpeed = slowedSpeed;
        currentRedEnemyHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentRedEnemyHealth / redEnemyHealth;
        if (currentRedEnemyHealth <= 0)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<RedSpearman>().Die();
            enemyChecker.CheckForRemainingEnemies();
        }

    }

    public void ResetScale()
    {
        // Resets the health bar to full.
        healthBar.fillAmount = 1;
    }
}
