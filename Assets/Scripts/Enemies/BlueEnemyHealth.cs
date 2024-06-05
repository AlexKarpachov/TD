using UnityEngine;

public class BlueEnemyHealth : MonoBehaviour
{
    [SerializeField] int blueEnemyHhealth = 100;
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50;

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

    EnemyMoneyCalculator moneyCalculator;
    private void OnEnable()
    {
        currentBlueEnemyHealth = blueEnemyHhealth;
    }

    void Start()
    {
        moneyCalculator = GetComponent<EnemyMoneyCalculator>();
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
        if (currentBlueEnemyHealth < 1)
        {
            Destroy(gameObject);
            moneyCalculator.MoneyDeposit();
        }
    }

    void HitBySmallMage()
    {
        currentBlueEnemyHealth -= sphere1Damage;
        if (currentBlueEnemyHealth < 1)
        {
            Destroy(gameObject);
            moneyCalculator.MoneyDeposit();
        }
    }
}
