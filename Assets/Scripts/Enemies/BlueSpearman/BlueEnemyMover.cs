using UnityEngine;

// This script is responsible for moving the BlueSpearman game object along a predefined route.
public class BlueEnemyMover : MonoBehaviour
{
    [SerializeField] EnemyMoneyCalculator moneyCalculator;
    [SerializeField] BlueEnemy blueEnemy;

    PlayerLives playerLivesScript;
    EnemyChecker enemyChecker; // checks the remaining amount of enemies on the scene

    public Transform target;

    public float speed = 8f;
    int wavePointIndex = 0;

    private void Start()
    {
        playerLivesScript = FindObjectOfType<PlayerLives>();
        enemyChecker = FindObjectOfType<EnemyChecker>();
        target = WaypointBlue.routeBlue[0]; // Set the initial target to the first waypoint in the route.
    }
    private void Update()
    {
        MoveBlueEnemy();
    }
    // Moves the BlueSpearman towards the target.
    void MoveBlueEnemy()
    {
        // Calculate the direction from the enemy to the target.
        Vector3 dir = target.position - transform.position;
        // Move the enemy in the direction of the target at the specified speed.
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        // Rotate the enemy to face the target
        transform.LookAt(target.transform.position);

        // Check if the enemy has reached the target
        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            GetNextPoint();
        }
    }
    // Gets the next waypoint in the route.
    void GetNextPoint()
    {
        // Check if the enemy has reached the end of the route.
        if (wavePointIndex >= WaypointBlue.routeBlue.Length - 1)
        {
            playerLivesScript.OutOfLives();
            moneyCalculator.MoneyWithdraw();
            blueEnemy.Die();
            if (RedEnemySpawner.lastwave == true) { enemyChecker.CheckForRemainingEnemies(); }
            return;
        }
        wavePointIndex++;
        // Set the target to the next waypoint in the route.
        target = WaypointBlue.routeBlue[wavePointIndex];
    }
    // Resets the enemy mover to the starting position
    public void ResetMover()
    {
        wavePointIndex = 0;
        if (WaypointBlue.routeBlue.Length > 0)
        {
            target = WaypointBlue.routeBlue[0];
        }
    }
}
