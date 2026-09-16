using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    //↓オーディオ再生用ソース↓
    public AudioSource BgmSource;
    public AudioSource SESource;



    //↓オーディオ再生用クリップ↓

    //---BGM---
    [Header("タイトルBGM")][SerializeField] public AudioClip Bgm_Title;
    [Header("ゲームBGM")][SerializeField] public AudioClip Bgm_Game;
    [Header("リザルトBGM")][SerializeField] public AudioClip Bgm_Result;


    //---効果音---
    [Header("スタート前の歓声")][SerializeField]public AudioClip SE_BeforeCheer;
    [Header("スタート前のカウントダウン音")][SerializeField] public AudioClip SE_CountDown;
    [Header("スタート音")][SerializeField] public AudioClip SE_Start;

    [Header("最終ラップ突入時の歓声")][SerializeField] public AudioClip SE_FinalCheer;
    [Header("最終ラップ突入時のゴング音")][SerializeField] public AudioClip SE_FinalGong;
    [Header("ゴール音")][SerializeField] public AudioClip SE_Goal;

    [Header("プレイヤー（ボート）のエンジン音")][SerializeField] public AudioClip SE_BoatEngine;

    [Header("ゲームスタートのボタン音")][SerializeField] public AudioClip SE_GameStartButton;
    //[Header("タイトルへ戻るボタン音")][SerializeField] public AudioClip SE_BackTitleButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
