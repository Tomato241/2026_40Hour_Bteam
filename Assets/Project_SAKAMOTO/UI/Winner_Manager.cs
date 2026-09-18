using UnityEngine;

public class Winner_Manager : MonoBehaviour
{
    public static bool isWinnerFinished = false; // 1位のゴールフラグ



// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        
    }


 void OnTriggerEnter(Collider other)
    {

        // 【デバッグ①】そもそも何かがゴールに触れたらコンソールに表示される
        Debug.Log("何かがゴール判定に触れました: " + other.name);

        //すでに1位がゴールしたら、
        //これ以降の処理をしない
        if (isWinnerFinished) return;
        //接触したプレイヤーのコンポーネント（情報）を読み取る
        Player_Info hitPlayer = other.GetComponent<Player_Info>();

        //ファイナルラップをゴールしたら
        if (RaceLap_Manager.LapCount_1p > 4
            || RaceLap_Manager.LapCount_2p > 4)
        {
            // 💡 プレイヤーオブジェクト自体ではなく、IDと名前だけを新しくデータとして保存する
            Player_Info.WinnerData = new WinnerRecord(hitPlayer.PlayerID, hitPlayer.playerName);

            isWinnerFinished = true; // 1位確定フラグを立てる
        }
    }

}
