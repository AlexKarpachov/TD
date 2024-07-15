using UnityEngine;

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
