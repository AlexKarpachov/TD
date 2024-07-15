using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSwordmanMover : MonoBehaviour
{
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] RedSwordman redSwordman;

    PlayerLives playerLivesScript;
    EnemyChecker enemyChecker;

    public Transform target;

    public float speed = 5f;
    int wavePointIndex = 0;

    private void Start()
    {
        playerLivesScript = FindObjectOfType<PlayerLives>();
        enemyChecker = FindObjectOfType<EnemyChecker>();
        target = WaypointRed.routeRed[0];
    }
    private void Update()
    {
        MoveRedEnemy();
    }
    void MoveRedEnemy()
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
        if (wavePointIndex >= WaypointRed.routeRed.Length - 1)
        {
            playerLivesScript.OutOfLives();
            moneyCalculator.MoneyWithdraw();
            enemyChecker.CheckForRemainingEnemies();
            redSwordman.Die();
            return;
        }
        wavePointIndex++;
        target = WaypointRed.routeRed[wavePointIndex];
    }
    public void ResetMover()
    {
        wavePointIndex = 0;
        if (WaypointRed.routeRed.Length > 0)
        {
            target = WaypointRed.routeRed[0];
        }
    }
}
