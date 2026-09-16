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

    //コイン取得によるスピードブースト
    private float boostMultiplier = 1f;
    private float boostTimer = 0f;

    //外部から呼び出してコントローラーを割り当てる
    public void AssignGamepad(Gamepad gamepad) => pad = gamepad;

    //外部（GetCoinCounterなど）から呼び出してスピードブーストをかける
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        boostMultiplier = multiplier;
        boostTimer = duration;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocityDirection = transform.forward;
        pad ??= Gamepad.current; //未割り当てなら暫定でcurrentを使う
    }

    void Update()
    {
        if (pad == null) return;

        //ブーストタイマーの更新
        if (boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f) boostMultiplier = 1f;
        }

        float targetSpeed = (pad.buttonSouth.isPressed ? maxMoveSpeed : 0f) * boostMultiplier;
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