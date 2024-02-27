using System.Collections;
using UnityEngine;

public class EnemySpowner : MonoBehaviour
{
    [SerializeField] int wave;
    [SerializeField] int enemiesInWave;
    [SerializeField] GameObject redEnemy;
    [SerializeField] Transform leftSpawnPoint;
    [SerializeField] Transform endPoint;

    bool gameOver = false;

    private void Start()
    {
        StartCoroutine(RedEnemySpowner());
    }

    IEnumerator RedEnemySpowner()
    {
        while (!gameOver)
        {
            GameObject tempRedEnemy = Instantiate(redEnemy, leftSpawnPoint.position, Quaternion.identity);
            EnemyLogic tempLogic = tempRedEnemy.GetComponent<EnemyLogic>();

            tempLogic.MoveTo(endPoint);
            yield return new WaitForSeconds(3);
        }
    }
}
