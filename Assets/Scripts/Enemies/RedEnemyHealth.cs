using UnityEngine;

public class RedEnemyHealth : MonoBehaviour
{
    [SerializeField] int redEnemyHealth = 100;
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50;

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

    EnemyMoneyCalculator moneyCalculator;
    private void OnEnable()
    {
        currentRedEnemyHealth = redEnemyHealth;
    }

    void Start()
    {
        moneyCalculator = GetComponent<EnemyMoneyCalculator>();
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
        if (currentRedEnemyHealth < 1)
        {
            Destroy(gameObject);
            moneyCalculator.MoneyDeposit();
        }
    }

    void HitBySmallMage()
    {
        currentRedEnemyHealth -= sphere1Damage;
        if (currentRedEnemyHealth < 1)
        {
            Destroy(gameObject);
            moneyCalculator.MoneyDeposit();
        }
    }
}
