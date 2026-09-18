using UnityEngine;

// 💡 勝者のデータだけを記録するための、Unityに消されない純粋なデータの箱
public class WinnerRecord
{
    public int PlayerID;
    public string PlayerName;

    public WinnerRecord(int id, string name)
    {
        PlayerID = id;
        PlayerName = name;
    }
}

public class Player_Info : MonoBehaviour
{
    [Header("プレイヤー識別用のID（0から始める）")]
    public int PlayerID;

    [Header("プレイヤーの名前（リザルト用）")]
    public string playerName;

    // 💡 保存先を「GameObject（MonoBehaviour）」ではなく「純粋なデータ（WinnerRecord）」にする
    public static WinnerRecord WinnerData;

    // 前回のデータをクリアする関数（必要に応じてタイトル等から呼ぶ）
    public static void ResetWinnerData()
    {
        WinnerData = null;
    }
}






