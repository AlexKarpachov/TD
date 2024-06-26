using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;
    
    PlayerLives playerLivesScript;
    EnemyMoneyCalculator moneyCalculator;

    private EnemySpawner enemySpawner;

    private void Start()
    {
        moneyCalculator = GetComponent<EnemyMoneyCalculator>();
        playerLivesScript = FindObjectOfType<PlayerLives>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void MoveTo(Transform destination)
    {
        navAgent.SetDestination(destination.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            playerLivesScript.OutOfLives();
            enemySpawner.OnEnemyDestroyed();
            moneyCalculator.MoneyWithdraw();
            Destroy(gameObject, 0.5f);
        }
    }
}
