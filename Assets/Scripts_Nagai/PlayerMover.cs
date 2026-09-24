using UnityEngine;
using UnityEngine.InputSystem;

// プレイヤー（ボート）の移動を制御するスクリプト
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("移動")]
    [SerializeField] private float maxMoveSpeed = 8f; // 移動速度の最大値
    [SerializeField] private float acceleration = 6f; // 加速量
    [SerializeField] private float deceleration = 4f; // 減速量

    [Header("回転")]
    [SerializeField] private float maxRotationSpeed = 120f; // 回転速度の最大値（度/秒）
    [SerializeField] private float rotationAcceleration = 200f; // 回転速度の変化量（加速度）

    [Header("水上の滑り（ドリフト）")]
    [SerializeField, Range(0.1f, 10f)] private float grip = 2f; // 滑りの強さ（大きいほど滑りにくくなる）

    [Header("壁バウンド")]
    [SerializeField] private string wallTag = "Wall"; // 壁オブジェクトに付けるタグ
    [SerializeField] private float bounceForce = 5f;  // 跳ね返る強さ
    [SerializeField, Range(0f, 1f)] private float bounceSpeedRetention = 0.5f; // 跳ね返り後に速度を何割残すか

    //エフェクトの再生
    [SerializeField] private ParticleSystem engineEffect;

    private Rigidbody rb;
    private Gamepad pad;
    private float speed;
    private float rotationSpeed;
    private Vector3 velocityDirection;

    //プレイヤーの角度を記録する変数
    [SerializeField] public static Vector3 playerAngles = Vector3.zero;

    // レース開始フラグの状態を読み取る変数（カウントダウン中は操作を受け付けないため）
    private RaceTime_Manager raceTime_manager;

    // コイン取得によるスピードブースト
    private float boostMultiplier = 1f;
    private float boostTimer = 0f;

    // サウンド状態管理フラグ
    private bool isEngineSoundPlaying = false;

    // 外部から呼び出してコントローラーを割り当てる
    public void AssignGamepad(Gamepad gamepad) => pad = gamepad;

    // 外部（GetCoinCounterなど）から呼び出してスピードブーストをかける
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        boostMultiplier = multiplier;
        boostTimer = duration;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        velocityDirection = transform.forward;
        pad ??= Gamepad.current; // 未割り当てなら暫定でcurrentを使う
    }

    void Start()
    {
        // RaceTime_Managerがついたオブジェクトを探し、IsStartの状態を参照できるようにする
        raceTime_manager = FindAnyObjectByType<RaceTime_Manager>();
    }

    void Update()
    {
        // レースがまだ始まっていない（カウントダウン中）は入力を受け付けない
        bool canControl = raceTime_manager == null || raceTime_manager.IsStart;

        if (pad == null || !canControl)
        {
            // 操作不可の間はアクセル入力を無視し、エンジン音や速度は慣性のみで処理する
            HandleEngineSound(false);

            float targetSpeedIdle = 0f;
            speed = Mathf.MoveTowards(speed, targetSpeedIdle, deceleration * Time.deltaTime);

            float targetRotationIdle = 0f;
            rotationSpeed = Mathf.MoveTowards(rotationSpeed, targetRotationIdle, rotationAcceleration * Time.deltaTime);
            return;
        }

        // ブーストタイマーの更新
        if (boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f) boostMultiplier = 1f;
        }

        bool isAccelerating = pad.buttonSouth.isPressed;

        // エンジン音の再生・停止制御
        HandleEngineSound(isAccelerating);
        // エンジンエフェクトの再生・停止制御
        if (engineEffect != null)
        {
            if (isAccelerating && !engineEffect.isPlaying)
            {
                engineEffect.Play();
            }
            else if (!isAccelerating && engineEffect.isPlaying)
            {
                engineEffect.Stop();
            }
        }

        // 移動・回転の計算
        float targetSpeed = (isAccelerating ? maxMoveSpeed : 0f) * boostMultiplier;
        speed = Mathf.MoveTowards(speed, targetSpeed, (targetSpeed > 0f ? acceleration : deceleration) * Time.deltaTime);

        float targetRotation = pad.leftStick.ReadValue().x * maxRotationSpeed;
        rotationSpeed = Mathf.MoveTowards(rotationSpeed, targetRotation, rotationAcceleration * Time.deltaTime);


        playerAngles = transform.rotation.eulerAngles;//プレイヤーの角度を保存する

    }

    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotationSpeed * Time.fixedDeltaTime, 0f));
        velocityDirection = Vector3.Slerp(velocityDirection, transform.forward, grip * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + velocityDirection * speed * Time.fixedDeltaTime);
    }

    private void HandleEngineSound(bool isAccelerating)
    {
        // アクセルを押している、または慣性で進んでいる時に鳴らす
        bool shouldPlay = isAccelerating || speed > 0.1f;

        if (shouldPlay && !isEngineSoundPlaying)
        {
            Sound_Manager.instance.PlayLoopBoatEngine_1PSE();
            isEngineSoundPlaying = true;
        }
        else if (!shouldPlay && isEngineSoundPlaying)
        {
            Sound_Manager.instance.StopBoatEngine_1PSE();
            isEngineSoundPlaying = false;
        }
    }

    private void OnDisable()
    {
        // 非表示・破棄された時にSEを停止
        if (isEngineSoundPlaying && Sound_Manager.instance != null)
        {
            Sound_Manager.instance.StopBoatEngine_1PSE();
            isEngineSoundPlaying = false;
        }
    }

    // コイン取得によるマックススピードの永続的な変更（バフ／デバフ用）
    public void ModifyMaxSpeed(float delta)
    {
        maxMoveSpeed = Mathf.Max(0f, maxMoveSpeed + delta);
        //速度は一定以上に下がらないようにする
        speed = Mathf.Min(speed, maxMoveSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(wallTag)) return;

        // 壁の法線方向を取得(複数接触点の平均)
        Vector3 normal = Vector3.zero;
        foreach (ContactPoint contact in collision.contacts)
        {
            normal += contact.normal;
        }
        normal.Normalize();

        // 進行方向を壁の法線で反射させる
        velocityDirection = Vector3.Reflect(velocityDirection, normal).normalized;

        // 速度は元の勢いに関係なく常に一定量で跳ね返す
        speed = bounceForce;
    }
}