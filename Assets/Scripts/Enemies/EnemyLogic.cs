using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    //GameObject spawnerObject;
    [SerializeField] NavMeshAgent navAgent;
    //[SerializeField] int redEnemyHP = 100;
    //[SerializeField] int blueEnemyHP = 100;
    EnemySpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
        /*
        spawnerObject = GameObject.Find("EnemySpawner");
        if (spawnerObject != null)
        {
            spawner = spawnerObject.GetComponent<EnemySpawner>();
        }
        else
        {
            Debug.LogError("EnemySpawnerObject not found!");
        }*/
    }

    public void MoveTo(Transform destination)
    {
        navAgent.SetDestination(destination.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            spawner.gameOver = true;
            navAgent.isStopped = true;
            Debug.Log("Game over!");
        }
    }
}
