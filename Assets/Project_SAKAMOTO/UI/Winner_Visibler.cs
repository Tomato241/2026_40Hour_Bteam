using UnityEngine;
using UnityEngine.UI; // UI（Canvas、Image、Text）を扱うために必要
using System.Collections; // コルーチン（アニメーション）を使うために必要

public class Winner_Visibler : MonoBehaviour
{
    [Header("リザルト表示用オブジェクト")]
    public GameObject winnnerObject;

    [Header("リザルト表示用レンダラー")]
    public SpriteRenderer winnnerSpriteRenderer;

    [Header("勝者の画像を表示するUIのImageコンポーネント")]
    public Sprite winnerSprite;

    [Header("勝利したプレイヤーの画像")]
    public Sprite[] characterSprites;

    [Header("名前が拡大アニメーションにかける時間（秒）")]
    public float animationDuration = 0.4f;

    private void Start()
    {
        if (Player_Info.WinnerData == null)
        {
            //Debug.LogError("勝者のデータが空っぽ（null）です！");
            return;
        }
        // 💡 保存された純粋なデータからIDと名前を読み取る（オブジェクトが消えていても大丈夫！）
        int winnerID = Player_Info.WinnerData.PlayerID;
        string winnerName = Player_Info.WinnerData.PlayerName;

        // 画像とテキストをセット
        winnnerSpriteRenderer.sprite = characterSprites[winnerID];

        //最後に確実に画像のサイズを 1 に固定する
        winnnerObject.transform.localScale = new Vector3(1.15f,1.15f,1.15f);
        //// 拡大アニメーションを開始
        //StartCoroutine(AnimateImageScale());
    }



    // 勝利したプレイヤー画像のサイズを拡大する処理
    private IEnumerator AnimateImageScale()
    {


        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / animationDuration;

            // 勢いをつけて最後にピタッと止まる減速カーブ
            float t = 1f - Mathf.Pow(1f - progress, 3);

            // 画像のサイズを徐々に等倍 (1, 1, 1) に近づける
            winnnerObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            yield return null; // 1フレーム待つ
                               //}


        }
    }
}
