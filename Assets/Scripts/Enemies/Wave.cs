using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    public GameObject redEnemyPrefab;
    public int redEnemiesAmountInWave;
    public float timeBetweenRedEnemies;

    public GameObject blueEnemyPrefab;
    public int blueEnemiesAmountInWave;
    public float timeBetweenBlueEnemies;
}
