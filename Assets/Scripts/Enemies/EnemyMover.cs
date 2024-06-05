using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;
    
    PlayerLives playerLivesScript;
    EnemyMoneyCalculator moneyCalculator;

    private void Start()
    {
        moneyCalculator = GetComponent<EnemyMoneyCalculator>();
        playerLivesScript = FindObjectOfType<PlayerLives>();
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
            moneyCalculator.MoneyWithdraw();
            Destroy(gameObject, 0.5f);
        }
    }
}
