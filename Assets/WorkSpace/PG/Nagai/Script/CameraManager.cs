using UnityEngine;

public class RailFollowCamera : MonoBehaviour
{
    [Header("追従対象(2人)")]
    public Transform targetA;
    public Transform targetB;

    [Header("レール(中央オブジェクト)")]
    public Transform railCenter;

    [Header("移動範囲(長方形)")]
    public Vector3 axisX = Vector3.right;    // 横方向の軸(ローカル)
    public Vector3 axisZ = Vector3.forward;  // 奥行き方向の軸(ローカル)
    public float halfWidth = 5f;   // axisX方向の可動範囲(片側)
    public float halfDepth = 3f;   // axisZ方向の可動範囲(片側)

    [Header("カメラオフセット(基準)")]
    public Vector3 baseOffset = new Vector3(0, 8, -10);
    public float smoothSpeed = 5f;

    [Header("距離に応じたズームアウト")]
    public float minDistance = 3f;
    public float maxDistance = 15f;
    public float maxExtraHeight = 10f;

    [Header("見る対象")]
    public bool lookAtMidpoint = true;

    void LateUpdate()
    {
        if (targetA == null || targetB == null || railCenter == null) return;

        // 1. 2人の中点
        Vector3 midpoint = (targetA.position + targetB.position) * 0.5f;

        // 2. 2つの軸をワールド方向に変換
        Vector3 worldAxisX = railCenter.TransformDirection(axisX.normalized);
        Vector3 worldAxisZ = railCenter.TransformDirection(axisZ.normalized);

        // 3. 中点を中央からの相対ベクトルにする
        Vector3 toMid = midpoint - railCenter.position;

        // 4. それぞれの軸に投影してからClamp(ここが「四角形」の肝)
        float distX = Vector3.Dot(toMid, worldAxisX);
        float distZ = Vector3.Dot(toMid, worldAxisZ);
        distX = Mathf.Clamp(distX, -halfWidth, halfWidth);
        distZ = Mathf.Clamp(distZ, -halfDepth, halfDepth);

        // 5. 2軸を合成して長方形内の点を求める
        Vector3 pointInRect = railCenter.position + worldAxisX * distX + worldAxisZ * distZ;

        // 6. ズーム係数計算(変更なし)
        float playerDist = Vector3.Distance(targetA.position, targetB.position);
        float t = Mathf.InverseLerp(minDistance, maxDistance, playerDist);

        Vector3 scaledOffset = baseOffset;
        scaledOffset.y = baseOffset.y + maxExtraHeight * t;
        Vector3 worldOffset = railCenter.rotation * scaledOffset;

        // 7. カメラの目標位置
        Vector3 desiredPos = pointInRect + worldOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        // 8. 向き
        if (lookAtMidpoint)
        {
            Quaternion desiredRot = Quaternion.LookRotation(midpoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, smoothSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (railCenter == null) return;

        Vector3 worldAxisX = railCenter.TransformDirection(axisX.normalized);
        Vector3 worldAxisZ = railCenter.TransformDirection(axisZ.normalized);
        Vector3 center = railCenter.position;

        // 長方形の4隅を計算して線で結ぶ
        Vector3 p1 = center + worldAxisX * halfWidth + worldAxisZ * halfDepth;
        Vector3 p2 = center + worldAxisX * halfWidth - worldAxisZ * halfDepth;
        Vector3 p3 = center - worldAxisX * halfWidth - worldAxisZ * halfDepth;
        Vector3 p4 = center - worldAxisX * halfWidth + worldAxisZ * halfDepth;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);

        if (targetA != null && targetB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(targetA.position, targetB.position);
        }
    }
}