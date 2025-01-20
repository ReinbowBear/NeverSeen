using System;
using System.Collections.Generic;
using UnityEngine;

public class GetBattle : MonoBehaviour
{
    [SerializeField] private WavesSpawner wavesSpawner;
    [SerializeField] private byte enemysCount;
    private int currentEnemys;
    private System.Random random;

    void Start()
    {
        random = new System.Random(DateTime.Now.Millisecond);

        WaveStruct waveStruct = new WaveStruct();
        List<WaveContainer> containers = new List<WaveContainer>();

        currentEnemys = 0;
        byte indexContainer = 0;
        while (currentEnemys < enemysCount)
        {
            int containerID = random.Next(0, Content.data.waves.containers.Length);
            containers.Add(Content.data.waves.containers[containerID]);

            currentEnemys += containers[indexContainer].enemys.Length+1;
            indexContainer++;
        }
        
        waveStruct.waveContainers = containers.ToArray();
        StartCoroutine(wavesSpawner.StartWave(waveStruct));
    }
}
