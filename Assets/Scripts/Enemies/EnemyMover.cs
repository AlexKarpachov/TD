using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] EnemyMoneyCalculator moneyCalculator;

    PlayerLives playerLivesScript;
    EnemySpawner enemySpawner;

    private void Start()
    {
        playerLivesScript = FindObjectOfType<PlayerLives>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void MoveTo(Transform destination)
    {
        navAgent.SetDestination(destination.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            playerLivesScript.OutOfLives();
            enemySpawner.OnEnemyDestroyed();
            moneyCalculator.MoneyWithdraw();
            Destroy(gameObject, 0.5f);
        }
    }
}
