using System;
using UnityEngine;

public class GetBattle : MonoBehaviour
{
    [SerializeField] private WavesSpawner wavesSpawner;
    private System.Random random;

    void Start()
    {
        random = new System.Random(DateTime.Now.Millisecond);

        WaveStruct waveStruct = new WaveStruct();
        waveStruct.enemys = new int[10];
        waveStruct.enemyDelays = new int[10];

        for (byte i = 0; i < waveStruct.enemys.Length; i++)
        {
            waveStruct.enemys[i] = random.Next(0, Content.data.enemys.containers.Length);
        }

        for (byte i = 0; i < waveStruct.enemyDelays.Length; i++)
        {
            waveStruct.enemyDelays[i] = random.Next(0, 10);
        }
        
        StartCoroutine(wavesSpawner.StartWave(waveStruct));
    }
}
