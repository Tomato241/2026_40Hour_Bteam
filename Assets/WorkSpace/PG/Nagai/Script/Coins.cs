using UnityEngine;

public enum CoinType
{
    Buff,   // マックススピード+1
    Debuff  // マックススピード-1
}

// コイン本体に付けるスクリプト
public class Coin : MonoBehaviour
{
    [SerializeField] private CoinType coinType = CoinType.Buff;
    [SerializeField] private float speedChangeAmount = 1f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMove player = other.GetComponent<PlayerMove>();
        if (player == null) return;

        float delta = (coinType == CoinType.Buff) ? speedChangeAmount : -speedChangeAmount;
        player.ModifyMaxSpeed(delta);

        Destroy(gameObject);
    }
}