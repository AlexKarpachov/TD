using UnityEngine;
using UnityEngine.UI;

public class RedSwordmanHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50; // The damage dealt by a sphere1 (Mage1 tower)
    [SerializeField] float slowedSpeed = 3f; // The slowed speed of the enemy when hit by a sphere1.
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] RedSwordmanMover mover;

    EnemyChecker enemyChecker;

    int currentRedSwordmanHealth;
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
    public Image healthBar; // The health bar UI image.
    public int redSwordmanHealth = 100;
    float originalSpeed;
    private RedEnemySpawner enemySpawner;

    private void OnEnable()
    {
        currentRedSwordmanHealth = redSwordmanHealth;
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
        // Reduce the enemy's health by 10
        // if the health is less than 0, destroys the enemy and pays $15 reward to the player
        currentRedSwordmanHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentRedSwordmanHealth / redSwordmanHealth;
        if (currentRedSwordmanHealth <= 0)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<RedSwordman>().Die();
            enemyChecker.CheckForRemainingEnemies();
        }
    }

    void HitBySmallMage()
    {
        // Called when the enemy is hit by sphere1 (Mage1)
        // Reduce the enemy's health by 15
        // if the health is less than 0, destroys the enemy and pays $15 reward to the player
        mover.speed = slowedSpeed;
        currentRedSwordmanHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentRedSwordmanHealth / redSwordmanHealth;
        if (currentRedSwordmanHealth <= 0)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<RedSwordman>().Die();
            enemyChecker.CheckForRemainingEnemies();
        }

    }

    public void ResetScale()
    {
        // Resets the health bar to full.
        healthBar.fillAmount = 1;
    }
}
