using System;
using System.Collections;
using TMPro; //TextMeshProを使うために必要 
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;//シーン管理に必要


public class RaceLap_Manager : MonoBehaviour
{
    public bool IsFinish;


    public static int LapCount_1p = 0;//1Pのラップ数カウント用変数
    public static int LapCount_2p = 0;//2Pのラップ数カウント用変数
    public static int LapCount_3p = 0;//3Pのラップ数カウント用変数
    public static int LapCount_4p = 0;//4Pのラップ数カウント用変数


    private int lastLap_1p = 0;
    private int lastLap_2p = 0;

    //private int lastLap_3p = 0;
    //private int lastLap_4p = 0;

    //ラップ数表示状態を保存する変数
    private int LapStatus_1p = 0;
    private int LapStatus_2p = 0;
    //private int LapStatus_3p = 0;
    //private int LapStatus_4p = 0;

    private bool isFinalLap;

    //逆走状態を保存する定数
    private const int REVERSEIMAGE_1P = 0;
    private const int REVERSEIMAGE_2P = 1;
    //private int ReverseImage_3p = 0;
    //private int ReverseImage_4p = 0;

    private const int NONEIMAGE_1P = 25;
    private const int NONEIMAGE_2P = 26;

    private const float flashingInterval=0.1f;


    float flashingStatus = 0;//逆走時の点滅演出
    float  flashingTimer = 0;//逆走時の点滅演出用タイマー

    [Header("最大ラップ数")][SerializeField] private int finalRap;


    [System.Serializable]
    public struct LapSprites
    {
        
        [Tooltip("表示名")]
        public string objectName;

        [Header("操作したいオブジェクト")]
        public SpriteRenderer targetSpriterenderer;

        [Header("変更したい画像")]
        public Sprite lapSprite;

    }

    [SerializeField] private LapSprites[]lapspriteList;


    [Header("1Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_1P;//1Pのラップ数表示用変数
    [Header("2Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_2P;//2Pのラップ数表示用変数
    [Header("3Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_3P;//3Pのラップ数表示用変数
    [Header("4Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_4P;//4Pのラップ数表示用変数

    


    [Header("Finishのテキスト")][SerializeField] private TextMeshProUGUI finishiText; //Finishの表示用変数

    [Header("Finishの表示オブジェクト")][SerializeField] private SpriteRenderer finishRenderer;
    [Header("Finishの表示オブジェクト")][SerializeField] private Sprite finishImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsFinish = false;
        isFinalLap = false;

        LapCount_1p = 0;
        LapCount_2p = 0;
        //lapCount_3p = 0;
        //lapCount_4p = 0;
        LapStatus_1p = 4;//インスペクターの1P表示初期位置
        LapStatus_2p = 9;//インスペクターの2P表示初期位置
        //LapStatus_3p = 14;//インスペクターの3P表示初期位置
        //LapStatus_4p = 19;//インスペクターの4P表示初期位置


        //finishiText.text = "";
        finishRenderer.enabled = false;//Finishの画像を非表示状態にセット
    }

    // Update is called once per frame
    void Update()
    {
    if (IsFinish) return;//レース終了時、以降の処理をしない



        //↓エラー防止用制限↓
        //ラップ数カウント
        if (LapCount_1p<1)
        {
            LapCount_1p = 1;
        }
        if (LapCount_2p < 1)
        {
            LapCount_2p = 1;
        }
        //if (LapCount_3p < 1)
        //{
        //    LapCount_3p = 1;
        //}
        //if (LapCount_4p < 1)
        //{
        //    LapCount_4p = 1;
        //}
        //ラップ数表示状態
        if (LapStatus_1p <= 4)
        {
            LapStatus_1p = 4;
        }
        else if (LapStatus_1p >= 8)
        {
            LapStatus_1p = 8;
        }
        if (LapStatus_2p <= 9)
        {
            LapStatus_2p = 9;
        }
        else if (LapStatus_2p >= 13)
        {
            LapStatus_2p = 13;
        }
        //if (LapStatus_3p <= 14)
        //{
        //    LapStatus_3p = 14;
        //}
        //else if (LapStatus_3p >= 18)
        //{
        //    LapStatus_3p = 18;
        //}
        //if (LapStatus_4p <= 19)
        //{
        //    LapStatus_4p = 19;
        //}
        //else if (LapStatus_4p >= 23)
        //{
        //    LapStatus_4p = 23;
        //}


        if (LapCount_1p==5|| LapCount_2p==5)
        {
           if(!isFinalLap)
            {
                Sound_Manager.instance.PlayFinalCheerSE();
                Sound_Manager.instance.PlayFinalLapSE();
                isFinalLap =true;
            }
        }


        //**********************************************
        //ラップ数増加・減少処理はこの下に書いてください
        //**********************************************
        //↓ラップ数増加・減少処理↓


        //↓ラップ表示のUI更新処理↓

        //ラップ数増加の場合
        //ラップ数が初期値ではなく、
        //前回と今回のフレームでラップ数が異なり、
        //前回より今回のフレームのラップ数が多ければ
        if (LapCount_1p!=1
            &&lastLap_1p!= LapCount_1p
            && lastLap_1p < LapCount_1p)
        {
            ++LapStatus_1p;                 //1Pのラップ数表示状態を1加算
            ChangeLapImage(LapStatus_1p);   //表示状態を反映
        }
        if (LapCount_2p != 1
            &&lastLap_2p != LapCount_2p
            &&lastLap_2p < LapCount_2p)
        {
            ++LapStatus_2p;                 //2Pのラップ数表示状態を1加算
            ChangeLapImage(LapStatus_2p);   //表示状態を反映
        }
        //    if (LapCount_3p != 1
        //      && lastLap_3p != LapCount_3p
        //      && lastLap_3p < LapCount_3p)
        //    {
        //        ++LapStatus_3p;                 //2Pのラップ数表示状態を1加算
        //        ChangeLapImage(LapStatus_3p);   //表示状態を反映
        //    }
        //    if (LapCount_4p != 1
        //      && lastLap_4p != LapCount_4p
        //      && lastLap_4p < LapCount_4p)
        //    {
        //        ++LapStatus_4p;                 //2Pのラップ数表示状態を1加算
        //        ChangeLapImage(LapStatus_4p);   //表示状態を反映
        //    }

        //ラップ数減少の場合
        //ラップ数が初期値ではなく、
        //前回と今回のフレームでラップ数が異なり、
        //前回より今回のフレームのラップ数が多ければ
        if (LapCount_1p < 4
           && lastLap_1p != LapCount_1p
           && lastLap_1p > LapCount_1p)
        {
            --LapStatus_1p;                 //1Pのラップ数表示状態を1減算
            ChangeLapImage(LapStatus_1p);   //表示状態を反映
        }
        if (LapCount_2p < 9
            && lastLap_2p != LapCount_2p
            && lastLap_2p > LapCount_2p)
        {
            --LapStatus_2p;                 //2Pのラップ数表示状態を1減算
            ChangeLapImage(LapStatus_2p);   //表示状態を反映
        }
        //     if (LapCount_3p < 14
        //         && lastLap_3p != LapCount_3p
        //         && lastLap_3p > LapCount_3p)
        //     {
        //         --LapStatus_3p;                 //1Pのラップ数表示状態を1減算
        //         ChangeLapImage(LapStatus_3p);   //表示状態を反映
        //     }
        //     if (LapCount_4p < 19
        //         && lastLap_4p != LapCount_4p
        //         && lastLap_4p > LapCount_4p)
        //     {
        //         --LapStatus_4p;                 //2Pのラップ数表示状態を1減算
        //         ChangeLapImage(LapStatus_4p);   //表示状態を反映
        //     }



        //↓逆走時のUI表示処理↓
        //
        if (ReverseRun_Manager.IsReverse)
        {
            
            if (flashingStatus == 0)
            {
                ChangeLapImage(REVERSEIMAGE_1P);
                flashingTimer += Time.deltaTime;
                if (flashingTimer > flashingInterval)
                {
                    ++flashingStatus;
                }
            }
            else
            {
                ChangeLapImage(NONEIMAGE_1P);
                flashingTimer -= Time.deltaTime;
                if (flashingTimer < -flashingInterval)
                {
                    flashingStatus=0;
                }
            }
        }
        else
        {
            ChangeLapImage(LapStatus_1p);
        }


        //if (2P逆走フラグ)
        //{
        //    ChangeLapImage(REVERSEIMAGE_1P);
        //}
        //else
        //{
        //    ChangeLapImage(LapStatus_2p);
        //}



        //↓Player1P～Player4P誰かがラップ数が↓
        //↓最大ラップ数を超えた時の処理      ↓
        if (LapCount_1p > finalRap
            ||LapCount_2p > finalRap)
        {
            StartCoroutine(VisibleFinishText());
        }

        //次回のフレームで比較するために保存
        lastLap_1p = LapCount_1p;
        lastLap_2p = LapCount_2p;
    }


    //リザルトシーンの遷移処理
    private void RoadResultScene()
    {
        SceneManager.LoadScene("ResultScene");//リザルトシーンに遷移
    }


    //Finishのテキスト表示
    private IEnumerator VisibleFinishText()
    {
        Sound_Manager.instance.StopFinalCheerSE();
        Sound_Manager.instance.StopBGM();
        Sound_Manager.instance.PlayGoalSE();//ゴール効果音再生
        IsFinish = true;//レース終了フラグをオン
        //finishiText.text = "FINISH!";//Finishのテキスト表示
        finishRenderer.enabled = true;//Finishの画像表示
        yield return new WaitForSeconds(5f);
        RoadResultScene();//リザルトシーンの遷移処理
    }


    //ラップ数表示の切り替え処理
    private void ChangeLapImage(int index)
    {
        //indexで受け取った番号のオブジェクトを対象に
        //indexで受け取った番号の画像に切り替える
        lapspriteList[index].targetSpriterenderer.sprite = lapspriteList[index].lapSprite;
    }



}
