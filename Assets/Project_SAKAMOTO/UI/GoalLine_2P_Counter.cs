using UnityEngine;

public class GoalLine_2P_Counter : MonoBehaviour
{
    [SerializeField] private bool hasPassFront;//フロントポイント通過フラグ
    [SerializeField] private bool hasPassBack;//バックポイント通過フラグ

    [SerializeField] private bool isFirst;//初回通過時のフラグ

    [SerializeField] private CoinSpawner[] coinSpawner; // CoinSpawnerの参照
    RaceLap_Manager racelap_manager;

    void Start()
    {
        hasPassFront = false;
        hasPassBack = false;
        isFirst = true;     //初回通過フラグをオン
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

                --RaceLap_Manager.LapCount_2p;//1Pのラップ数を減算
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
                    isFirst = false;   //初回通過フラグをオフ
                    return;
                }
                //ゴールラインの通過が2回目以降なら
                else
                {
                    ++RaceLap_Manager.LapCount_2p;//2Pのラップ数を加算
                    hasPassBack = false;          //バックポイント通過フラグをオフ
                    hasPassFront = false;         //フロントポイント通過フラグをオフ

                    if (RaceLap_Manager.LapCount_2p >= RaceLap_Manager.LapCount_1p)
                    {
                        SpawnCoinsAll();
                    }
                    return;             //処理が条件にかからないようにここで終了する
                }
            }
        }



    }

    private void SpawnCoinsAll()
    {
        foreach (var spawner in coinSpawner)
        {
            spawner.SpawnCoins();
        }
    }
}
