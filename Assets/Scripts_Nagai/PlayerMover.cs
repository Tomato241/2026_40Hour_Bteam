using UnityEngine;
using UnityEngine.InputSystem;

//プレイヤー（ボート）の移動を制御するスクリプト
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("移動")]
    [SerializeField] private float maxMoveSpeed = 8f;
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float deceleration = 4f;

    [Header("回転")]
    [SerializeField] private float maxRotationSpeed = 120f;
    [SerializeField] private float rotationAcceleration = 200f;

    [Header("水上の滑り（ドリフト）")]
    [SerializeField, Range(0.1f, 10f)] private float grip = 2f;

    private Rigidbody rb;
    private Gamepad pad;
    private float speed;
    private float rotationSpeed;
    private Vector3 velocityDirection;

    //外部から呼び出してコントローラーを割り当てる
    public void AssignGamepad(Gamepad gamepad) => pad = gamepad;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocityDirection = transform.forward;
        pad ??= Gamepad.current; //未割り当てなら暫定でcurrentを使う
    }

    void Update()
    {
        if (pad == null) return;

        float targetSpeed = pad.buttonSouth.isPressed ? maxMoveSpeed : 0f;
        speed = Mathf.MoveTowards(speed, targetSpeed, (targetSpeed > 0f ? acceleration : deceleration) * Time.deltaTime);

        float targetRotation = pad.leftStick.ReadValue().x * maxRotationSpeed;
        rotationSpeed = Mathf.MoveTowards(rotationSpeed, targetRotation, rotationAcceleration * Time.deltaTime);
    }

    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotationSpeed * Time.fixedDeltaTime, 0f));
        velocityDirection = Vector3.Slerp(velocityDirection, transform.forward, grip * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + velocityDirection * speed * Time.fixedDeltaTime);
    }
}