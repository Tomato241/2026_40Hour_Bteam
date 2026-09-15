using System;
using System.Collections;
using TMPro; //TextMeshProを使うために必要 
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;//シーン管理に必要


public class RaceLap_Manager : MonoBehaviour
{
    public bool IsFinish;


    public int LapCount_1p = 0;//1Pのラップ数カウント用変数
    public int LapCount_2p = 0;//2Pのラップ数カウント用変数
    public int LapCount_3p = 0;//3Pのラップ数カウント用変数
    public int LapCount_4p = 0;//4Pのラップ数カウント用変数


    private int lastLap_1p = 0;
    private int lastLap_2p = 0;

    //private int lastLap_3p = 0;
    //private int lastLap_4p = 0;


    private int LapStatus_1p = 0;
    private int LapStatus_2p = 0;
    //private int LapStatus_3p = 0;
    //private int LapStatus_4p = 0;

    private int reverseImage_1p = 0;
    private int reverseImage_2p = 0;
    //private int ReverseImage_3p = 0;
    //private int ReverseImage_4p = 0;

    [System.Serializable]
    public struct LapSprites
    {
        
        [Tooltip("表示名")]
        public string objectName;

        [Header("操作したいオブジェクト")]
        public SpriteRenderer targetRenderer;

        [Header("変更したい画像")]
        public Sprite lapSprite;

    }

    [SerializeField] private LapSprites[]lapspriteList;


    [Header("1Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_1P;//1Pのラップ数表示用変数
    [Header("2Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_2P;//2Pのラップ数表示用変数
    [Header("3Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_3P;//3Pのラップ数表示用変数
    [Header("4Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_4P;//4Pのラップ数表示用変数

    [Header("最大ラップ数")][SerializeField] private int finalRap;


    [Header("Finishのテキスト")][SerializeField] private TextMeshProUGUI finishiText; //Finishの表示用変数

    [Header("Finishの表示オブジェクト")][SerializeField] private SpriteRenderer finishRenderer;
    //[Header("Finishの表示オブジェクト")][SerializeField] private Sprite finishImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsFinish = false;

        LapCount_1p = 1;
        LapCount_2p = 1;
        //lapCount_3p = 1;
        //lapCount_4p = 1;
        LapStatus_1p = 4;
        LapStatus_2p = 9;
        //LapStatus_3p = 14;
        //LapStatus_4p = 19;


        //finishiText.text = "";
        finishRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFinish) return;

        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            ++LapCount_1p;
        }
        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            ++LapCount_2p;
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            --LapCount_1p;
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            --LapCount_2p;
        }

        if(LapCount_1p<1)
        {
            LapCount_1p = 1;
        }
        if (LapCount_2p < 1)
        {
            LapCount_2p = 1;
        }



        if (LapCount_1p!=1
            &&lastLap_1p!= LapCount_1p
            && lastLap_1p < LapCount_1p)
        {
            ++LapStatus_1p;
            ChangeLapImage(LapStatus_1p);
        }
        if (LapCount_2p != 1
            &&lastLap_2p != LapCount_2p
            &&lastLap_2p < LapCount_2p)
        {
            ++LapStatus_2p;
            ChangeLapImage(LapStatus_2p);
        }


        if (LapCount_1p < 4
           && lastLap_1p != LapCount_1p
           && lastLap_1p > LapCount_1p)
        {
            --LapStatus_1p;
            ChangeLapImage(LapStatus_1p);
        }
        if (LapCount_2p < 9
            && lastLap_2p != LapCount_2p
            && lastLap_2p > LapCount_2p)
        {
            --LapStatus_2p;
            ChangeLapImage(LapStatus_2p);
        }

        if(LapStatus_1p<=4)
        {
            LapStatus_1p = 4;
        }
        else if(LapStatus_1p>=8)
        {
            LapStatus_1p = 8;
        }
        if (LapStatus_2p <= 9)
        {
            LapStatus_2p = 9;
        }
        else if (LapStatus_2p >= 14)
        {
            LapStatus_2p = 14;
        }


        if (Keyboard.current.upArrowKey.isPressed)
        {
            ChangeLapImage(reverseImage_1p);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            ChangeLapImage(reverseImage_2p);
        }


        //Player1P～Player4P誰かがラップ数が最大ラップ数を超えたら
        if (LapCount_1p >= finalRap
            ||LapCount_2p >= finalRap)
        {
            StartCoroutine(VisibleFinishText());
        }


        lastLap_1p = LapCount_1p;
        lastLap_2p = LapCount_2p;
    }


    //リザルトシーンの遷移処理
    private void RoadResultScene()
    {
        SceneManager.LoadScene("Result");//リザルトシーンに遷移
    }


    //Finishのテキスト表示
    private IEnumerator VisibleFinishText()
    {
        IsFinish = true;
        //finishiText.text = "FINISH!";//Finishのテキスト表示
        finishRenderer.enabled = true;//Finishの画像表示
        yield return new WaitForSeconds(3f);
        RoadResultScene();//リザルトシーンの遷移処理
    }

    private void ChangeLapImage(int index)
    {
        lapspriteList[index].targetRenderer.sprite = lapspriteList[index].lapSprite;
    }



}
