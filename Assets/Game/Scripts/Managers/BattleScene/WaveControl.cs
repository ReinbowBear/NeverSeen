using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
                StartCoroutine(CreateEnemy(mapData.enemys[waveLeft][i]));
            }

            waveLeft++;
            yield return new WaitForSeconds(mapData.enemys[waveLeft-1].Length * 5);
        }

        EventBus.Invoke<OnEndLevel>();
    }

    public IEnumerator CreateEnemy(string enemyName)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(enemyName);
        yield return handle;

        Vector3 randomPos = new Vector3(MyRandom.random.Next(-8, 8), 0, MyRandom.random.Next(-8, 8));
        Instantiate(handle.Result, randomPos, Quaternion.identity);

        var release = handle.Result.AddComponent<ReleaseOnDestroy>();
        release.handle = handle;
    }
}
