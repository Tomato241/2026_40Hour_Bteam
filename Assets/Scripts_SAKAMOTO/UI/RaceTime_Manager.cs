using System;
using System.Collections;
using TMPro; //TextMeshProを使うために必要 
using UnityEngine;
using UnityEngine.UI; //Imageを使うために必要

public class RaceTime_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;//カウントダウン用テキスト

    [SerializeField] private TextMeshProUGUI gameTimerText;//ゲーム内時間用テキスト

    //カウントダウン用の画像を保存する配列
    [SerializeField] private Sprite[] timerSprites;

    private string gametimerStr;//gameTimerの文字列を編集するための変数

    [SerializeField] private SpriteRenderer countdownRenderer;//カウントダウン用切り替え変数

    [SerializeField] private Sprite countdown_1_Image;//カウントダウン1用画像変数
    [SerializeField] private Sprite countdown_2_Image;//カウントダウン2用画像変数
    [SerializeField] private Sprite countdown_3_Image;//カウントダウン3用画像変数
    [SerializeField] private Sprite countdown_start_Image;//Start用画像変数

    public bool IsStart = false;

    //Game_Manager game_manager;
    //game_manager.isStart;

    public float gameTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //時間表示をする構造体に変換
        TimeSpan gameClockTimer = TimeSpan.FromSeconds(gameTimer);
        //00(分):00:00(秒)に変換する
        gameTimerText.text = gameClockTimer.ToString(@"m\:ss\:ff");

        //カウントダウン処理開始
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStart)//ゲームスタートしたら
        {
            //ゲーム内タイマーをカウント開始
            gameTimer += Time.deltaTime;

            //時間表示をする構造体に変換
            TimeSpan gameClockTimer = TimeSpan.FromSeconds(gameTimer);
            //00(分):00:00(秒)に変換する
            gameTimerText.text = gameClockTimer.ToString(@"m\:ss\:ff");

        }


    }

    //カウントダウン処理
    private IEnumerator CountDown()
    {
        countdownText.text = "3";                 //テキストの場合
        //countdownRenderer = countdown_3_Image;////画像の場合
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";                 //テキストの場合
        //countdownRenderer = countdown_2_Image;////画像の場合
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";                 //テキストの場合
        //countdownRenderer = countdown_1_Image;////画像の場合
        yield return new WaitForSeconds(1f);

        countdownText.text = "START!";                //テキストの場合
        //countdownRenderer = countdown_start_Image;////画像の場合
        IsStart = true;

        yield return new WaitForSeconds(1f);
        countdownText.text = "";
    }
}
