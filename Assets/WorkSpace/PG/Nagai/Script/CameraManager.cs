using UnityEngine;

public class RailFollowCamera : MonoBehaviour
{
    [Header("追従対象(2人・自動取得)")]
    public Transform targetA;
    public Transform targetB;

    [Header("追従対象のタグ")]
    public string tagA = "Player1";
    public string tagB = "Player2";

    [Header("レール(中央オブジェクト)")]
    public Transform railCenter;

    [Header("移動範囲(長方形)")]
    public Vector3 axisX = Vector3.right;
    public Vector3 axisZ = Vector3.forward;
    public float halfWidth = 5f;
    public float halfDepth = 3f;

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
        // ターゲットが未取得なら毎フレーム探しにいく(スポーンタイミングのズレに対応)
        if (targetA == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag(tagA);
            if (found != null) targetA = found.transform;
        }
        if (targetB == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag(tagB);
            if (found != null) targetB = found.transform;
        }

        // 2P分揃っていない場合は、いる方だけ単独で追う
        if (targetA == null && targetB == null) return;
        if (railCenter == null) return;

        Vector3 midpoint;
        float playerDist;

        if (targetA != null && targetB != null)
        {
            midpoint = (targetA.position + targetB.position) * 0.5f;
            playerDist = Vector3.Distance(targetA.position, targetB.position);
        }
        else
        {
            // 片方しかいない場合はそのプレイヤー位置を中点扱いにする
            Transform onlyTarget = targetA != null ? targetA : targetB;
            midpoint = onlyTarget.position;
            playerDist = minDistance; // ズームアウトさせない
        }

        Vector3 worldAxisX = railCenter.TransformDirection(axisX.normalized);
        Vector3 worldAxisZ = railCenter.TransformDirection(axisZ.normalized);

        Vector3 toMid = midpoint - railCenter.position;

        float distX = Vector3.Dot(toMid, worldAxisX);
        float distZ = Vector3.Dot(toMid, worldAxisZ);
        distX = Mathf.Clamp(distX, -halfWidth, halfWidth);
        distZ = Mathf.Clamp(distZ, -halfDepth, halfDepth);

        Vector3 pointInRect = railCenter.position + worldAxisX * distX + worldAxisZ * distZ;

        float t = Mathf.InverseLerp(minDistance, maxDistance, playerDist);

        Vector3 scaledOffset = baseOffset;
        scaledOffset.y = baseOffset.y + maxExtraHeight * t;
        Vector3 worldOffset = railCenter.rotation * scaledOffset;

        Vector3 desiredPos = pointInRect + worldOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

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