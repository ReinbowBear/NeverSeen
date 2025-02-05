using System.Collections;
using UnityEngine;

public class WavesSpawner : MonoBehaviour
{
    [SerializeField] private EntityManager entityManager;
    private byte currentWaveID;

    public IEnumerator StartWave(WaveStruct waveStruct)
    {
        currentWaveID = 0;
        while (currentWaveID != waveStruct.waveContainers.Length)
        {
            for (byte i = 0; i < waveStruct.waveContainers[currentWaveID].enemys.Length; i++)
            {
                entityManager.AddEntity(Content.instance.enemys.GetItemByName(waveStruct.waveContainers[currentWaveID].enemys[i]));
                yield return new WaitForSeconds(0.25f);
            }

            yield return new WaitForSeconds((waveStruct.waveContainers[currentWaveID].enemys.Length + 1) * 5);
            currentWaveID++;
        }
    }

    private void CheckEndBattle(MyEvent.OnEnemyDeath _)
    {
        if (currentWaveID == 0)
        {
            Debug.Log("бой закончен");
        }
    }


    void OnEnable()
    {
        EventBus.Add<MyEvent.OnEnemyDeath>(CheckEndBattle);
    }

    void OnDisable()
    {
        EventBus.Remove<MyEvent.OnEnemyDeath>(CheckEndBattle);
    }
}


[System.Serializable]
public struct WaveStruct
{
    public WaveContainer[] waveContainers;
}
