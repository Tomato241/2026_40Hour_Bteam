using UnityEngine;

public class GoalLine_1P_Counter : MonoBehaviour
{
   [SerializeField] private bool hasPassFront;//フロントポイント通過フラグ
   [SerializeField] private bool hasPassBack;//バックポイント通過フラグ

    [SerializeField] private bool isFirst;//初回通過時のフラグ

    RaceLap_Manager racelap_manager;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasPassFront = false;
        hasPassBack = false;
        isFirst = true;     //初回通過フラグをオン
        racelap_manager =FindAnyObjectByType<RaceLap_Manager>();
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
            //バックポイントを通過していない状態で、
            //フロントポイントを通過したら
            if (!hasPassBack)
            {
                hasPassFront = true;//フロントポイント通過フラグをオン
                return;             //処理が条件にかからないようにここで終了する
            }
            //バックポイントを通過していた状態で、
            //フロントポイントを通過したら
            else if (hasPassBack)
            {

                --racelap_manager.LapCount_1p;//1Pのラップ数を減算
                hasPassFront = false;    //フロントポイント通過フラグをオフ
                hasPassBack = false;     //バックポイント通過フラグをオフ
                return;                  //処理が条件にかからないようにここで終了する

            }
        }


        if (other.CompareTag("GoalBack"))
        {
            //フロントポイントを通過していない状態で、
            //バックポイントを通過したら
            if (!hasPassFront)
            {
                hasPassBack = true;
                return;
            }
            //バックポイントを通過していない状態で、
            //フロントポイントを通過したら
            else if (hasPassFront)
            {
                //ゴールラインの通過が初めてなら
                if (isFirst)
                {
                    isFirst=false;   //初回通過フラグをオフ
                    return;
                }
                //ゴールラインの通過が2回目以降なら
                else
                {
                    ++racelap_manager.LapCount_1p;//1Pのラップ数を加算
                    hasPassBack = false;          //バックポイント通過フラグをオフ
                    hasPassFront = false;         //フロントポイント通過フラグをオフ
                    return;             //処理が条件にかからないようにここで終了する
                }
            }
        }
    }

}
