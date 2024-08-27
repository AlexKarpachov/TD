using UnityEngine;
using UnityEngine.UI;

public class BlueSwordmanHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50; // The damage dealt by a sphere1 (Mage1 tower)
    [SerializeField] float slowedSpeed = 3f; // The slowed speed of the enemy when hit by a sphere1.
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] BlueSwordmanMover mover;

    EnemyChecker enemyChecker;

    int currentBlueSwordmanHealth;
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
    public Image healthBar; // The health bar UI image.
    public int blueSwordmanHealth = 100;
    float originalSpeed;
    private BlueEnemySpawner enemySpawner;

    private void OnEnable()
    {
        currentBlueSwordmanHealth = blueSwordmanHealth;
    }

    void Start()
    {
        enemySpawner = FindObjectOfType<BlueEnemySpawner>();
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
        // Reduce the enemy's health by 10
        // if the health is less than 0, destroys the enemy and pays $15 reward to the player
        currentBlueSwordmanHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentBlueSwordmanHealth / blueSwordmanHealth;
        if (currentBlueSwordmanHealth <= 0)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<BlueSwordman>().Die();
            enemyChecker.CheckForRemainingEnemies();
        }
    }

    void HitBySmallMage()
    {
        // Called when the enemy is hit by sphere1 (Mage1)
        // Reduce the enemy's health by 15
        // if the health is less than 0, destroys the enemy and pays $15 reward to the player
        mover.speed = slowedSpeed;
        currentBlueSwordmanHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentBlueSwordmanHealth / blueSwordmanHealth;
        if (currentBlueSwordmanHealth <= 0)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<BlueSwordman>().Die();
            enemyChecker.CheckForRemainingEnemies();
        }
    }

    public void ResetScale()
    {
        // Resets the health bar to full.
        healthBar.fillAmount = 1;
    }
}
