using UnityEngine;
using System.Collections;

public class GetCoinCounter : MonoBehaviour
{
    [Header("コイン演出")]
    [SerializeField] private float rotateSpeed = 180f; // 1秒あたりの回転角度
    [SerializeField] private float respawnDelay = 5f;  // 再出現までの時間
    [SerializeField] private GameObject pickupEffect;  // 取得時のパーティクル（任意）
    [SerializeField] private AudioClip pickupSound;    // 取得音（任意）

    private Collider coinCollider;
    private MeshRenderer coinRenderer;

    void Awake()
    {
        coinCollider = GetComponent<Collider>();
        coinRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        //くるくる回転させる（Y軸中心）
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerMove mover = other.GetComponent<PlayerMove>();
        if (mover == null) return;

        //取得数をカウント（コインカウンターが別途あればそちらを呼ぶ）
        //CoinManager.Instance.AddCoin();

        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
        }
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        coinCollider.enabled = false;
        coinRenderer.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        coinCollider.enabled = true;
        coinRenderer.enabled = true;
    }
}