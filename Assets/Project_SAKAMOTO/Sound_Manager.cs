
using UnityEngine;
using UnityEngine.Audio;


public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager instance;

    //↓オーディオ再生用ソース↓
    public AudioSource BgmSource;
    public AudioSource SESource;

    public AudioSource CheerSource;
    public AudioSource BoatEngine_1PSource;
    public AudioSource BoatEngine_2PSource;
    //public AudioSource BoatEngine_3PSource;
    //public AudioSource BoatEngine_4PSource;



    //↓オーディオ再生用クリップ↓

    //---BGM---
    [Header("タイトルBGM")][SerializeField] public AudioClip Bgm_Title;
    [Header("ゲームBGM")][SerializeField] public AudioClip Bgm_Game;
    [Header("リザルトBGM")][SerializeField] public AudioClip Bgm_Result;


    //---効果音---
    [Header("スタート前の歓声")][SerializeField] public AudioClip SE_BeforeCheer;
    [Header("スタート前のカウントダウン音")][SerializeField] public AudioClip SE_CountDown;
    [Header("スタート音")][SerializeField] public AudioClip SE_Start;

    [Header("ラップ音")][SerializeField] public AudioClip SE_Lap;

    [Header("最終ラップ突入時の歓声")][SerializeField] public AudioClip SE_FinalCheer;
    [Header("最終ラップ突入音")][SerializeField] public AudioClip SE_FinalLap;
    [Header("ゴール音")][SerializeField] public AudioClip SE_Goal;

    [Header("プレイヤー（ボート）のエンジン音")][SerializeField] public AudioClip SE_BoatEngine;

    [Header("ゲームスタートのボタン音")][SerializeField] public AudioClip SE_GameStartButton;
    //[Header("タイトルへ戻るボタン音")][SerializeField] public AudioClip SE_BackTitleButton;

    private void Awake()
    {
        instance = this;
    }


    public void PlayTitleBGM()
    {
        // clipに音源をセットして再生
        BgmSource.clip = Bgm_Title;
        BgmSource.Play();
    }

    public void PlayGameBGM()
    {
        // clipに音源をセットして再生
        BgmSource.clip = Bgm_Game;
        BgmSource.Play();
    }

    public void StopBGM()
    {
        BgmSource.Stop();
    }

    public void PlayResultBGM()
    {
        // clipに音源をセットして再生
        BgmSource.clip = Bgm_Result;
        BgmSource.Play();
    }

    public void PlayBeforeCheerSE()
    {
        CheerSource.PlayOneShot(SE_BeforeCheer);
    }

    public void StopBeforeCheerSE()
    {
        CheerSource.Stop();
    }

    public void PlayCountDownSE()
    {
        SESource.PlayOneShot(SE_CountDown);
    }

    public void PlayStartSE()
    {
        SESource.PlayOneShot(SE_Start);
    }

    public void PlayLapSE()
    {
        SESource.PlayOneShot(SE_Lap);
    }

    public void PlayFinalCheerSE()
    {
        CheerSource.PlayOneShot(SE_FinalCheer);
    }

    public void StopFinalCheerSE()
    {
        CheerSource.Stop();
    }

    public void PlayFinalLapSE()
    {
        SESource.PlayOneShot(SE_FinalLap);
    }

    public void PlayGoalSE()
    {
        SESource.PlayOneShot(SE_Goal);
    }

    //1Pのボートのエンジン音をループ再生処理
    public void PlayLoopBoatEngine_1PSE()
    {
        BoatEngine_1PSource.clip = SE_BoatEngine;
        BoatEngine_1PSource.loop = true; // ループ再生
        BoatEngine_1PSource.Play();
    }

    //1Pのボートのエンジン音のループ再生を止める処理
    public void StopBoatEngine_1PSE()
    {
        BoatEngine_1PSource.Stop();
    }

    //2Pのボートのエンジン音をループ再生処理
    public void PlayLoopBoatEngine_2PSE()
    {
        BoatEngine_2PSource.clip = SE_BoatEngine;
        BoatEngine_2PSource.loop = true; // ループ再生
        BoatEngine_2PSource.Play();
    }

    //2Pのボートのエンジン音のループ再生を止める処理
    public void StopBoatEngine_2PSE()
    {
        BoatEngine_2PSource.Stop();
    }

    //ゲームスタートのボタン音の再処理
    public void GameStartButtonSE()
    {
        SESource.PlayOneShot(SE_GameStartButton);
    }
}