using UnityEngine;

/* manages the spawning of blue enemies in a game. 
 * It stores the necessary data for a wave of blue enemies, including the prefabs, amounts, and timing.
 * */
[System.Serializable]
public class WaveBlue
{
    public GameObject blueSpearmanPrefab;
    public int blueSpearmanInWave;
    public GameObject blueSwordmanPrefab;
    public int blueSwordmanInWave;
    public float timeBetweenBlueEnemies;

    public BlueSpearmanPool blueSpearmanPool;
    public BlueSwordmanPool blueSwordmanPool;
}
