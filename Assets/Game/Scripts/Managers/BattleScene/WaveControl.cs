using System.Collections;
using UnityEngine;

public class WaveControl : MonoBehaviour
{
    void Start()
    {
        MapFactory.GenerateMap();
        StartCoroutine(GoWave(MapFactory.mapData));
    }

    private IEnumerator GoWave(MapData mapData)
    {
        byte waveLeft = 0;
        while (waveLeft != mapData.wavesCount)
        {
            for (byte i = 0; i < mapData.enemys[waveLeft].Length; i++)
            {
                var handle = Address.GetAssetByName(mapData.enemys[waveLeft][i]);
                yield return new WaitUntil(() => handle.IsCompleted);

                GameObject enemy = handle.Result;
                enemy.transform.position = new Vector3(MyRandom.random.Next(-5, 5), 0, MyRandom.random.Next(-5, 5));
            }

            waveLeft++;
            yield return new WaitForSeconds(mapData.enemys[waveLeft-1].Length * 5);
        }

        EventBus.Invoke<OnEndLevel>();
    }
}
