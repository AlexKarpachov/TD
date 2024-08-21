using UnityEngine;
using UnityEngine.UI;

public class BlueEnemyHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50; // The damage dealt by a sphere1 (Mage1 tower)
    [SerializeField] float slowedSpeed = 5f; // The slowed speed of the enemy when hit by a sphere1.
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] BlueEnemyMover mover;

    EnemyChecker enemyChecker;

    int currentBlueEnemyHealth; // health of blue spearman
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
    public Image healthBar; // The health bar UI image.
    public int blueEnemyHealth = 100;
    float originalSpeed;
    private BlueEnemySpawner enemySpawner;

    private void OnEnable()
    {
        currentBlueEnemyHealth = blueEnemyHealth;
    }

    private void Start()
    {
        enemyChecker = FindObjectOfType<EnemyChecker>();
        enemySpawner = FindObjectOfType<BlueEnemySpawner>();
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
        currentBlueEnemyHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentBlueEnemyHealth / blueEnemyHealth;
        if (currentBlueEnemyHealth <= 0)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<BlueEnemy>().Die();
            if (RedEnemySpawner.lastwave == true) { enemyChecker.CheckForRemainingEnemies(); }
        }
    }

    void HitBySmallMage()
    {
        // Called when the enemy is hit by sphere1 (Mage1)
        // Reduce the enemy's health by 30
        // if the health is less than 0, destroys the enemy and pays $10 reward to the player
        mover.speed = slowedSpeed;
        currentBlueEnemyHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentBlueEnemyHealth / blueEnemyHealth;
        if (currentBlueEnemyHealth <= 0)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<BlueEnemy>().Die();
            if (RedEnemySpawner.lastwave == true) { enemyChecker.CheckForRemainingEnemies(); }
        }
    }

    public void ResetScale()
    {
        // Resets the health bar to full.
        healthBar.fillAmount = 1;
    }
}
