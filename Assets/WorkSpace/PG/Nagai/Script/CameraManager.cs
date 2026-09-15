using UnityEngine;

public class RailFollowCamera : MonoBehaviour
{
    [Header("追従対象")]
    public Transform target;          // プレイヤー

    [Header("レール(中央オブジェクト)")]
    public Transform railCenter;      // コース中央のオブジェクト
    public Vector3 railDirection = Vector3.right; // レールが伸びている方向(ローカル軸)
    public float railHalfLength = 5f; // レールの片側の長さ(スライド可能範囲)

    [Header("カメラオフセット")]
    public Vector3 offset = new Vector3(0, 8, -10); // レール軸に対する相対オフセット
    public float smoothSpeed = 5f;

    [Header("見る対象")]
    public bool lookAtTarget = true;

    void LateUpdate()
    {
        if (target == null || railCenter == null) return;

        // 1. レールのワールド方向を求める(中央オブジェクトの回転を反映)
        Vector3 worldRailDir = railCenter.TransformDirection(railDirection.normalized);

        // 2. プレイヤーの位置をレール軸に投影し、中心からの距離を測る
        Vector3 toTarget = target.position - railCenter.position;
        float projectedDist = Vector3.Dot(toTarget, worldRailDir);

        // 3. スライド範囲をClamp(コースの端で止める)
        projectedDist = Mathf.Clamp(projectedDist, -railHalfLength, railHalfLength);

        // 4. レール上の対応点を求める
        Vector3 pointOnRail = railCenter.position + worldRailDir * projectedDist;

        // 5. オフセットを加えてカメラの目標位置を決定
        //    offsetはrailCenterの向きに合わせて回転させる(コースが斜めでも対応)
        Vector3 worldOffset = railCenter.rotation * offset;
        Vector3 desiredPos = pointOnRail + worldOffset;

        // 6. 滑らかに追従
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        if (lookAtTarget)
        {
            Quaternion desiredRot = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, smoothSpeed * Time.deltaTime);
        }
    }

    // シーンビューでレールとオフセットの範囲を確認できるように
    void OnDrawGizmosSelected()
    {
        if (railCenter == null) return;
        Vector3 worldRailDir = railCenter.TransformDirection(railDirection.normalized);
        Vector3 p1 = railCenter.position + worldRailDir * railHalfLength;
        Vector3 p2 = railCenter.position - worldRailDir * railHalfLength;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawSphere(p1, 0.2f);
        Gizmos.DrawSphere(p2, 0.2f);
    }
}