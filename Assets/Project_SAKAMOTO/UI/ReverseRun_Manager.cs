using UnityEngine;
using UnityEngine.InputSystem;

public class ReverseRun_Manager : MonoBehaviour
{

    
    public static bool IsReverse = false;//逆走状態を保存するフラグ

    [Header("逆走(StageL)の最小判定角度")]
    [SerializeField] private float minL_ReverseAngle;
    [Header("逆走(StageL)の最大判定角度")]
    [SerializeField] private float maxL_ReverseAngle;
    [Header("逆走(StageR)の最小判定角度")]
    [SerializeField] private float minR_ReverseAngle;
    [Header("逆走(StageR)の最大判定角度")]
    [SerializeField] private float maxR_ReverseAngle;
    [Header("逆走(StageCenterTop)の最小判定角度")]
    [SerializeField] private float minTop_ReverseAngle;
    [Header("逆走(StageCenterTop)の最大判定角度")]
    [SerializeField] private float maxTop_ReverseAngle;
    [Header("逆走(StageCenterBottom)の最小判定角度")]
    [SerializeField] private float minBottom_ReverseAngle;
    [Header("逆走(StageCenterBottom)の最大判定角度")]
    [SerializeField] private float maxBottom_ReverseAngle;


    
    void FixedUpdate()
    {
        //↓プレイヤーからレイを飛ばして、
        //触れたオブジェクトによって逆走しているか判定する処理


        //オブジェクト自身の真下に向かって3ピクセルのレイに、
        //何か触れたら
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3f))
        {
            //触れたオブジェクトの名前で分岐
            switch (hit.collider.gameObject.name)
            {
                case "StageLeft":
                    if (Gamepad.current.aButton.isPressed
                    && PlayerMove.playerAngles.y > minL_ReverseAngle
                    && PlayerMove.playerAngles.y < maxL_ReverseAngle)
                    {
                        IsReverse = true;//逆走フラグをオン
                    }
                    else
                    {
                        IsReverse = false;//逆走フラグをオフ
                    }
                    break;

                case "StageRight":
                    if (Gamepad.current.aButton.isPressed
                    && PlayerMove.playerAngles.y > minR_ReverseAngle
                    && PlayerMove.playerAngles.y < maxR_ReverseAngle
                    || Gamepad.current.aButton.isPressed
                    && PlayerMove.playerAngles.y > minR_ReverseAngle-270
                    && PlayerMove.playerAngles.y < maxR_ReverseAngle-270
                    )
                    {
                        IsReverse = true;//逆走フラグをオン
                    }
                    else
                    {
                        IsReverse = false;//逆走フラグをオフ
                    }

                    break;

                case "StageCenterTop":
                    if (Gamepad.current.aButton.isPressed
                    && PlayerMove.playerAngles.y > minTop_ReverseAngle
                    && PlayerMove.playerAngles.y < maxTop_ReverseAngle)
                    {
                        IsReverse = true;//逆走フラグをオン
                    }
                    else
                    {
                        IsReverse = false;//逆走フラグをオフ
                    }

                    break;

                case "StageCenterBottom":
                    if (Gamepad.current.aButton.isPressed
                    && PlayerMove.playerAngles.y > minBottom_ReverseAngle
                    && PlayerMove.playerAngles.y < maxBottom_ReverseAngle)
                    {
                        IsReverse = true;//逆走フラグをオン
                    }
                    else
                    {
                        IsReverse = false;//逆走フラグをオフ
                    }

                    break;
            }

        }
    }

    // デバッグ用に、シーン画面でRay（光線）の長さを視覚的に確認できるようにする
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 3f);
    }
}
