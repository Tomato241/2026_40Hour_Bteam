using System.Collections;
using TMPro; //TextMeshProを使うために必要 
using UnityEngine;
using UnityEngine.UI; //Imageを使うために必要

public class RaceTime_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;//カウントダウン用テキスト

    [SerializeField] private TextMeshProUGUI gameTimerText;//ゲーム内時間用テキスト

    private string gametimerStr;//gameTimerの文字列を編集するための変数

    [SerializeField] private Sprite countdownSprite;//カウントダウン用画像

    public bool isStart=false;

    //Game_Manager game_manager;
    //game_manager.isStart;

    public float gameTimer=0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ゲーム内タイマーをfloatからstring型に変換して
        //テキスト表示用変数に保存
        gameTimerText.text = gameTimer.ToString("00.00");

        //テキスト表示用変数の文字列の情報を、文字列編集用変数に保存
        gametimerStr = gameTimerText.text;
        //編集する文字列に含まれている「.」を「"」に変換して、
        //テキスト表示用変数に代入
        gameTimerText.text = gametimerStr.Replace(".", "\"");
        //カウントダウン処理開始
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)//ゲームスタートしたら
        {
            //ゲーム内タイマーをカウント開始
            gameTimer += Time.deltaTime;
            
            //ゲーム内タイマーをfloatからstring型に変換して
            //テキスト表示用変数に保存
            gameTimerText.text = gameTimer.ToString("00.00");

            //テキスト表示用変数の文字列の情報を、文字列編集用変数に保存
            gametimerStr = gameTimerText.text;
            //編集する文字列に含まれている「.」を「"」に変換して、
            //テキスト表示用変数に代入
            gameTimerText.text = gametimerStr.Replace(".","\"");
            
        }


    }

    private IEnumerator CountDown()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "START!";
        isStart = true;
        yield return new WaitForSeconds(1f);
        countdownText.text = "";
    }
}
