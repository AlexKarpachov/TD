using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSwordmanMover : MonoBehaviour
{
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] BlueSwordman blueSwordman;

    EnemyChecker enemyChecker;
    PlayerLives playerLivesScript;

    public Transform target;

    public float speed = 8f;
    int wavePointIndex = 0;

    private void Start()
    {
        enemyChecker = FindObjectOfType<EnemyChecker>();
        playerLivesScript = FindObjectOfType<PlayerLives>();
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
            blueSwordman.Die();
            enemyChecker.CheckForRemainingEnemies();
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
