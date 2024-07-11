using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyMover : MonoBehaviour
{
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    PlayerLives playerLivesScript;
    EnemySpawner enemySpawner;
    public Transform target;

    public float speed = 8f;
    int wavePointIndex = 0;

    private void Start()
    {
        playerLivesScript = FindObjectOfType<PlayerLives>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        target = WaypointBlue.routeBlue[0];
    }
    private void Update()
    {
        MoveBlueEnemy();
    }
    void MoveBlueEnemy()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        transform.LookAt(target.transform.position);

        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            GetNextPoint();
        }
    }
    void GetNextPoint()
    {
        if (wavePointIndex >= WaypointBlue.routeBlue.Length - 1)
        {
            playerLivesScript.OutOfLives();
            moneyCalculator.MoneyWithdraw();
            enemySpawner.OnEnemyDestroyed(gameObject);
            return;
        }
        wavePointIndex++;
        target = WaypointBlue.routeBlue[wavePointIndex];
    }
    public void ResetMover()
    {
        wavePointIndex = 0;
        if (WaypointBlue.routeBlue.Length > 0)
        {
            target = WaypointBlue.routeBlue[0];
        }
    }
}
