using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefabBuff;   // バフ用コインのプレハブ
    [SerializeField]
    private GameObject coinPrefabDebuff; // デバフ用コインのプレハブ
    [SerializeField, Range(0f, 1f)]
    private float buffSpawnChance = 0.5f; // バフコインが出現する確率
    [SerializeField, Min(1)]
    private int spawnCount = 1;          // 1回のゴールで生成するコインの数
    [SerializeField]
    private float positionMinX = 0f;
    [SerializeField]
    private float positionMaxX = 0f;
    [SerializeField]
    private float positionMinZ = 0f;
    [SerializeField]
    private float positionMaxZ = 0f;
    [SerializeField]
    private float positionY = 0f;

    /// <summary>
    /// ゴール時などに外部から呼び出してコインを生成する
    /// </summary>
    public void SpawnCoins()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            CoinSpawn();
        }
    }

    private void CoinSpawn()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(positionMinX, positionMaxX),
            positionY,
            Random.Range(positionMinZ, positionMaxZ)
        );

        GameObject prefabToSpawn = (Random.value < buffSpawnChance) ? coinPrefabBuff : coinPrefabDebuff;

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}