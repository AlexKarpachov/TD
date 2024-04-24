using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] ParticleSystem shootingSparksParticles;
    //[SerializeField] int redEnemyHP = 100;
    //[SerializeField] int blueEnemyHP = 100;
    EnemySpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
    }

    public void MoveTo(Transform destination)
    {
        navAgent.SetDestination(destination.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            shootingSparksParticles.Play();
            Destroy(other.gameObject, 1f);
        }

        if (other.gameObject.tag == "Finish")
        {
            spawner.gameOver = true;
            navAgent.isStopped = true;
            Debug.Log("Game over!");
        }
    }

    //here should a method of HP reduction and enemy destroying
}
