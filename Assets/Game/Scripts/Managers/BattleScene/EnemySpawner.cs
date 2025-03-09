using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRadius;
    [Space]
    [SerializeField] private string[] enemys;

    private async Task GetEnemy(string newEnemy)
    {
        Vector3 spawnPosition = GetRandomPos();
        GameObject enemy = await Address.GetAssetByName(newEnemy);

        enemy.transform.position = spawnPosition;
    }

    private Vector3 GetRandomPos()
    {
        Vector3 randomPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
        randomPosition.y = transform.position.y;

        return randomPosition;
    }
}
