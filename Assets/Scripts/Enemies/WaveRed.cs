using UnityEngine;

/* manages the spawning of red enemies in a game. 
 * It stores the necessary data for a wave of red enemies, including the prefabs, amounts, and timing.
 * */
[System.Serializable]
public class WaveRed
{
    public GameObject redSpearmanPrefab;
    public int redSpearmenAmountInWave;
    public GameObject redSwordmanPrefab;
    public int redSwordmenAmountInWave;
    public float timeBetweenRedEnemies;

    public RedSpearmanPool redSpearmanPool;
    public RedSwordmanPool redSwordmanPool;
}
