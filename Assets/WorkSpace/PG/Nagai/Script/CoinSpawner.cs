using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;
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
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CoinSpawn();
        }
    }
    void CoinSpawn()
    {
        // コインをランダムな位置に生成する
        Vector3 spawnPosition = new Vector3
            (Random.Range(positionMinX, positionMaxX), positiony, 
             Random.Range(positionMinZ, positionMaxZ));
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
}
