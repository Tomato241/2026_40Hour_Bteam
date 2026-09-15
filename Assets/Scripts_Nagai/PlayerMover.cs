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
    private float grip = 2f;

    private float currentSpeed = 0f;
    private float currentRotationSpeed = 0f;
    private Vector3 velocityDirection;
    private Rigidbody rb;

    void Start()
    {
        velocityDirection = transform.forward;
        rb = GetComponent<Rigidbody>();

        if (Gamepad.current == null)
        {
            Debug.Log("ゲームパッドが接続されていません");
        }
    }

    void Update()
    {
        if (Gamepad.current == null) return;

        //--- 前進の加減速処理（数値計算のみ、ここではtransformを触らない） ---
        float targetSpeed = Gamepad.current.buttonSouth.isPressed ? maxMoveSpeed : 0f;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed,
            (targetSpeed > 0f ? acceleration : deceleration) * Time.deltaTime);

        //--- 回転速度の計算のみ（実際の回転はFixedUpdateで行う） ---
        Vector2 moveInput = Gamepad.current.leftStick.ReadValue();
        float targetRotationSpeed = moveInput.x * maxRotationSpeed;
        currentRotationSpeed = Mathf.MoveTowards(currentRotationSpeed, targetRotationSpeed, rotationAcceleration * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //--- 船体の回転（物理エンジン経由） ---
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, currentRotationSpeed * Time.fixedDeltaTime, 0f));

        //--- 進行方向を船体の向きに少しずつ近づける ---
        velocityDirection = Vector3.Slerp(velocityDirection, transform.forward, grip * Time.fixedDeltaTime);

        //--- 実際の移動 ---
        rb.MovePosition(rb.position + velocityDirection * currentSpeed * Time.fixedDeltaTime);
    }
}