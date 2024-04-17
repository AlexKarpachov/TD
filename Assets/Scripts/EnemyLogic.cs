using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{

    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] int redEnemyHP = 100;
    [SerializeField] int blueEnemyHP = 100;

    public void MoveTo(Transform destination)
    {
        navAgent.SetDestination(destination.position);
    }
}
