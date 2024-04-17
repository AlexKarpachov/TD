using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    [SerializeField] int hitPoints = 30;
    [SerializeField] int currentEnemyHealth;

    int startingEnemyHealth = 100;
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentEnemyHealth = startingEnemyHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentEnemyHealth -= hitPoints;
        if (currentEnemyHealth <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
}
