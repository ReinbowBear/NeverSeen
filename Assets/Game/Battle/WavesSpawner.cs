using UnityEngine;

public class WavesSpawner : MonoBehaviour
{
    [SerializeField] private WaveStruct[] waves;

    private byte waveID;
    private byte enemyID;
    private byte delay;

    private int leftToSpawn;

    void Start()
    {
        leftToSpawn = waves[0].enemys.Length;
    }

    public void LaunchWave() //задержка перед спауном должна быть минимум 1 ход, иначе всё поломается
    {
        delay++;
        if (leftToSpawn > 0)
        {
            if (delay == waves[waveID].enemyDelays[enemyID])
            {
                Instantiate(waves[waveID].enemys[enemyID], waves[waveID].enemySpawners[enemyID].transform.position, Quaternion.identity);
                enemyID++;
                leftToSpawn--;
                delay = 0;
            }
        }
        else
        {
            if (waveID < waves.Length -1)
            {
                waveID++;
                leftToSpawn = waves[waveID].enemys.Length;
                enemyID = 0;
                delay = 0;
            }
        }
    }
}


[System.Serializable]
public class WaveStruct //хорошая идея сделать из них скриптбл обджекты, с заранее заготовлеными волнами
{
    public GameObject[] enemys;
    public GameObject[] enemySpawners;
    public byte[] enemyDelays;
}
