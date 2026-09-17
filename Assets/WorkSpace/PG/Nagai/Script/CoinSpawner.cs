using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefabBuff;   // バフ用コインのプレハブ
    [SerializeField]
    private GameObject coinPrefabDebuff; // デバフ用コインのプレハブ
    [SerializeField, Range(0f, 1f)]
    private float buffSpawnChance = 0.5f; // バフコインが出現する確率
    [SerializeField]
    private float positionMinX = 0f;
    [SerializeField]
    private float positionMaxX = 0f;
    [SerializeField]
    private float positionMinZ = 0f;
    [SerializeField]
    private float positionMaxZ = 0f;
    [SerializeField]
    private float positiony = 0f;

    void Start()
    {
        CoinSpawn();
    }
    void Update()
    {

    }
    void CoinSpawn()
    {
        // コインをランダムな位置に生成する
        Vector3 spawnPosition = new Vector3
            (Random.Range(positionMinX, positionMaxX), positiony,
             Random.Range(positionMinZ, positionMaxZ));

        // バフ／デバフどちらのコインを出すかをランダムに決定する
        GameObject prefabToSpawn = (Random.value < buffSpawnChance) ? coinPrefabBuff : coinPrefabDebuff;

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}