using System.Collections;
using UnityEngine;

public class WavesSpawner : MonoBehaviour
{
    [SerializeField] private EntityFactory entityFactory;
    private byte currentEnemyID;


    public IEnumerator StartWave(WaveStruct waveStruct)
    {
        currentEnemyID = 0;
        while (currentEnemyID != waveStruct.enemys.Length)
        {
            yield return new WaitForSeconds(waveStruct.enemyDelays[currentEnemyID]);

            //entityFactory.GetEnemy(waveStruct.enemys[currentEnemyID]);
            currentEnemyID++;
        }
    }
}


[System.Serializable]
public struct WaveStruct
{
    public int[] enemys;
    public int[] enemyDelays;
}
