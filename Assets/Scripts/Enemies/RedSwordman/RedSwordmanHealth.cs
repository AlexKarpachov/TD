using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RedSwordmanHealth : MonoBehaviour
{
    [SerializeField] int arrowDamage = 25;
    [SerializeField] int sphere1Damage = 50;
    [SerializeField] float slowedSpeed = 3f;
    [SerializeField] float slowingDuration = 2f;
    [SerializeField] int currentRedSwordmanHealth;
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] RedSwordmanMover mover;

    EnemyChecker enemyChecker;
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
        currentRedSwordmanHealth -= arrowDamage;
        healthBar.fillAmount = (float)currentRedSwordmanHealth / redSwordmanHealth;
        if (currentRedSwordmanHealth < 1)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<RedSwordman>().Die();
            enemyChecker.CheckForRemainingEnemies();
        }
    }

    void HitBySmallMage()
    {
        originalSpeed = mover.speed;
        mover.speed = slowedSpeed;
        currentRedSwordmanHealth -= sphere1Damage;
        healthBar.fillAmount = (float)currentRedSwordmanHealth / redSwordmanHealth;
        if (currentRedSwordmanHealth < 1)
        {
            moneyCalculator.MoneyDeposit();
            gameObject.GetComponent<RedSwordman>().Die();
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
