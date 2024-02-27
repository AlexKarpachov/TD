using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class EnemyLogic : MonoBehaviour
{
    
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] int redEnemyHP = 100;

    public void MoveTo(Transform destination)
    {
        navAgent.SetDestination(destination.position);
    }
}
