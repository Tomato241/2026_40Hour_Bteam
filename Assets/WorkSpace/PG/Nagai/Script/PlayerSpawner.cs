using UnityEngine;
using UnityEngine.InputSystem;

// 接続されているコントローラーを検出し、1P・2P用オブジェクトをそれぞれスポーンするスクリプト
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerPrefabs; // 要素0:1P用オブジェクト, 要素1:2P用オブジェクト

    [SerializeField]
    private Transform[] spawnPoints; // 要素0:1Pのスポーン位置, 要素1:2Pのスポーン位置

    private static readonly string[] playerTags = { "Player1", "Player2" };

    private GameObject[] spawnedPlayers = new GameObject[2];

    void Start()
    {
        SpawnConnectedPlayers();
    }

    void SpawnConnectedPlayers()
    {
        int playerCount = Mathf.Min(Gamepad.all.Count, playerPrefabs.Length, spawnPoints.Length);

        for (int i = 0; i < playerCount; i++)
        {
            GameObject player = Instantiate(playerPrefabs[i], spawnPoints[i].position, spawnPoints[i].rotation);
            player.tag = playerTags[i]; // 1P用オブジェクトにはPlayer1、2P用にはPlayer2

            PlayerMove move = player.GetComponent<PlayerMove>();
            if (move != null)
            {
                move.AssignGamepad(Gamepad.all[i]);
            }

            spawnedPlayers[i] = player;

            Debug.Log($"{playerTags[i]} spawned with controller: {Gamepad.all[i].displayName}");
        }
    }
}