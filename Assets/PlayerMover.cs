using UnityEngine;
using UnityEngine.InputSystem;

//プレイヤーの移動を制御するスクリプト
public class PlayerMove : MonoBehaviour
{
    [Header("移動")]
    [SerializeField]
    private float maxMoveSpeed = 8f;
    [SerializeField]
    private float acceleration = 6f;
    [SerializeField]
    private float deceleration = 4f;

    [Header("回転")]
    [SerializeField]
    private float maxRotationSpeed = 120f;
    [SerializeField]
    private float rotationAcceleration = 200f;

    [Header("水上の滑り（ドリフト）")]
    [SerializeField, Range(0.1f, 10f)]
    private float grip = 2f; //数値が小さいほど滑る（船体が向いた方向に速度が追いつくまでの速さ）

    private float currentSpeed = 0f;
    private float currentRotationSpeed = 0f;
    private Vector3 velocityDirection; //実際に進んでいる方向（見た目の向きとは別管理）

    void Start()
    {
        velocityDirection = transform.forward;

        if (Gamepad.current == null)
        {
            Debug.Log("ゲームパッドが接続されていません");
        }
    }

    void Update()
    {
        if (Gamepad.current == null) return;

        //--- 前進の加減速処理 ---
        float targetSpeed = Gamepad.current.buttonSouth.isPressed ? maxMoveSpeed : 0f;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed,
            (targetSpeed > 0f ? acceleration : deceleration) * Time.deltaTime);

        //--- 船体（見た目）の回転処理 ---
        Vector2 moveInput = Gamepad.current.leftStick.ReadValue();
        float targetRotationSpeed = moveInput.x * maxRotationSpeed;
        currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, targetRotationSpeed, rotationAcceleration * Time.deltaTime);
        transform.Rotate(0f, currentRotationSpeed * Time.deltaTime, 0f);

        //--- 進行方向（velocityDirection）を船体の向きに少しずつ近づける ---
        //ここがポイント：即座に一致させず、gripの速さで追従させることで水上を滑る感覚が出る
        velocityDirection = Vector3.Slerp(velocityDirection, transform.forward, grip * Time.deltaTime);

        //--- 実際の移動は「船体の向き」ではなく「velocityDirection」を使う ---
        transform.position += velocityDirection * currentSpeed * Time.deltaTime;
    }
}