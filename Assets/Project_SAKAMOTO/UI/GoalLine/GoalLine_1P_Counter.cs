using UnityEngine;

public class GoalLine_1P_Counter : MonoBehaviour
{
   [SerializeField] private bool hasPassFront;//ゴールフロント地点通過フラグ
   [SerializeField] private bool hasPassBack;//ゴールバック地点通過フラグ

   [SerializeField] private bool hasPassMidFront;//中間バック地点通過フラグ

    [SerializeField] private bool hasPassMidBack;//中間フロント地点通過フラグ

    [SerializeField] private bool isBackward;//逆走状態地点フラグ



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasPassFront = false;
        hasPassBack = false;
        hasPassMidFront = false;
        hasPassMidBack = false;
        isBackward = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ゴール判定処理
    void OnTriggerEnter(Collider other)
    {
        //GoalFront→GoalBackの順(順周り)に通過すればラップ数が加算
        //GoalBack→GoalFrontの順(逆周り)に通過すればラップ数が減算

        if (other.CompareTag("GoalFront"))
        {
            //ゴールバック地点を通過していない、
            //前回のゴールから逆走していない状態で、
            //ゴールフロント地点を通過したら
            if (!hasPassBack && !isBackward)
            {
                hasPassFront = true;//ゴールフロント地点通過フラグをオン
                return;             //処理が条件にかからないようにここで終了する
            }
            //ゴールバック地点・中間バック地点を通過した状態で、
            //ゴールフロント地点を通過したら
            else if (hasPassBack&&hasPassMidBack)
            {
                hasPassBack = false;            //ゴールバック地点通過フラグをオフ
                isBackward = false;             //逆走状態フラグをオフ
                hasPassFront = false;           //ゴールフロント地点通過フラグをオフ
                hasPassMidBack = false;         //中間バック地点通過フラグをオフ
                return;                         //終了
            }
            //ゴールフロント地点を通過した状態で、
            //前回のゴールから逆走しながら、
            //ゴールフロント地点を通過したら
            else if (hasPassFront&& isBackward)
            {
                --RaceLap_Manager.LapCount_1p;//1Pのラップ数を減算
                hasPassBack = false;            //ゴールバック地点通過フラグをオフ
                isBackward = false;             //逆走状態フラグをオフ
                hasPassFront = false;           //ゴールフロント地点通過フラグをオフ
                hasPassMidBack = false;         //中間バック地点通過フラグをオフ
                return;                         //終了
            }

        }



        if (other.CompareTag("MidFront"))
        {
            //ゴールフロント地点・ゴールバック地点を通過していて、
            //前回のゴールから逆走していない状態で、
            //中間フロント地点を通過したら
            if (hasPassFront&& !hasPassBack
                &&!isBackward)
            {
                hasPassMidFront = true;//中間フロント地点通過フラグをオン
                return;                //終了
            }
            //ゴールフロント地点・中間バック地点を通過していないが、
            //ゴールバック地点を通過している状態なら、
            else if (!hasPassFront&&!hasPassMidBack
                 && hasPassBack)
            {
                isBackward = true;//逆走状態フラグをオン
                return;           //終了
            }
            //逆走後にゴールして全てのフラグがリセットされた状態なら
            else if(!hasPassFront && !hasPassBack
                && !hasPassMidBack && !hasPassMidFront
                && !isBackward)
            {
                hasPassFront = true;          //ゴールフロント地点通過フラグをオン  
                hasPassMidFront = true;       //中間フロント地点通過フラグをオン  
                return;                       //終了
            }


        }

        if (other.CompareTag("MidBack"))
        {
            //ゴールバック地点・ゴールフロント地点を通過していて、
            //前回のゴールから逆走していない状態で、
            //中間バック地点を通過したら
            if (hasPassBack && !hasPassFront
                && !isBackward)
            {
                hasPassMidBack = true;          //中間バック地点通過フラグをオン
                return;                         //終了
            }
            //ゴールバック地点・中間フロント地点を通過していないが、
            //ゴールフロント地点を通過している状態なら、
            else if (!hasPassBack && !hasPassMidFront
                && hasPassFront)
            {
                isBackward = true;              //逆走状態フラグをオン
                return;                         //終了
            }
            //逆走後にゴールして全てのフラグがリセットされた状態なら
            else if (!hasPassBack && !hasPassFront
                && !hasPassMidFront &&!hasPassMidBack
                &&!isBackward)
            {
                hasPassBack = true;             // ゴールバック地点通過フラグをオフ
                hasPassMidBack = true;          // 中間バック地点通過フラグをオフ
                return;                         //終了
            }



        }

        if (other.CompareTag("GoalBack"))
        {
            //ゴールフロント地点を通過していない、
            //前回のゴールから逆走していない状態で、
            //ゴールバック地点を通過したら
            if (!hasPassFront && !isBackward)
            {
                hasPassBack = true;             //ゴールバック地点通過フラグをオン
                return;                         //終了
            }
            //ゴールフロント地点・中間フロント地点を通過した状態で、
            //ゴールフロント地点を通過したら
            else if (hasPassFront && hasPassMidFront)
            {
                hasPassFront = false;           //ゴールバック地点通過フラグをオフ
                hasPassBack = false;            //ゴールバック地点通過フラグをオフ
                hasPassMidFront = false;        //中間フロント地点通過フラグをオン
                isBackward = false;             //逆走状態フラグをオフ
                return;                         //終了
            }
            //ゴールバック地点を通過した状態で、
            //前回のゴールから逆走しながら、
            //ゴールバック地点を通過したら
            else if (hasPassBack&& isBackward)
            {
                ++RaceLap_Manager.LapCount_1p;//1Pのラップ数を加算
                hasPassFront = false;           //ゴールバック地点通過フラグをオフ
                hasPassBack = false;            //ゴールバック地点通過フラグをオフ
                hasPassMidFront = false;        //中間フロント地点通過フラグをオン
                isBackward = false;             //逆走状態フラグをオフ
                return;                         //終了
            }


        }

        if (other.CompareTag("GoalCenter"))
        {
            //ゴールフロント地点・中間フロント地点を通過した状態で、
            //前回のゴールから逆走状態でなければ
            if (hasPassFront && hasPassMidFront
                &&!isBackward)
            {
                ++RaceLap_Manager.LapCount_1p;//1Pのラップ数を加算
                hasPassBack = false;          //バックポイント通過フラグをオフ
                hasPassMidFront = false;      //中間フロント地点通過フラグをオフ
                return;                       //終了
            }
            //ゴールバック地点・中間バック地点を通過した状態で、
            //前回のゴールから逆走状態でなければ
            else if (hasPassBack && hasPassMidBack
                && !isBackward)
            {
                --RaceLap_Manager.LapCount_1p;//1Pのラップ数を減算
                hasPassFront = false;         //フロントポイント通過フラグをオフ
                hasPassMidBack = false;       //中間バック地点通過フラグをオフ
                return;                       //終了
            }
        }






    }
}
