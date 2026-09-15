using UnityEngine;
using TMPro; //TextMeshProを使うために必要 
using UnityEngine.SceneManagement;//シーン管理に必要
using System.Collections;
using UnityEngine.InputSystem;


public class RaceLap_Manager : MonoBehaviour
{
    public int LapCount_1p = 0;//1Pのラップ数カウント用変数
    public int LapCount_2p = 0;//2Pのラップ数カウント用変数
    public int LapCount_3p = 0;//3Pのラップ数カウント用変数
    public int LapCount_4p = 0;//4Pのラップ数カウント用変数

    [Header("1Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_1P;//1Pのラップ数表示用変数
    [Header("2Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_2P;//2Pのラップ数表示用変数
    [Header("3Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_3P;//3Pのラップ数表示用変数
    [Header("4Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_4P;//4Pのラップ数表示用変数

    [Header("最大ラップ数")][SerializeField] private int maxRap;


    [Header("Finishのテキスト")][SerializeField] private TextMeshProUGUI finishiText; //Finishの表示用変数

    [Header("Finishの画像")][SerializeField] private SpriteRenderer finishRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LapCount_1p = 1;
        LapCount_2p = 1;
        //lapCount_3p = 1;
        //lapCount_4p = 1;
        lapCountText_1P.text = LapCount_1p.ToString();
        lapCountText_2P.text = LapCount_2p.ToString();
        //lapCountText_3P.text = lapCount_3p.ToString();
        //lapCountText_4P.text = lapCount_4p.ToString();
        finishiText.text = "";
        finishRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        lapCountText_1P.text = LapCount_1p.ToString();//ラップ数(int)をstringに変換
        lapCountText_2P.text = LapCount_2p.ToString();//ラップ数(int)をstringに変換

        //Player1P～Player4P誰かがラップ数が最大ラップ数を超えたら
        if (LapCount_1p >= maxRap
            ||LapCount_2p >= maxRap)
        {
            StartCoroutine(VisibleFinishText());
            RoadResultScene();//リザルトシーンの
        }


    }


    //リザルトシーンの遷移処理
    private void RoadResultScene()
    {
        SceneManager.LoadScene("Result");//リザルトシーンに遷移
    }


    //Finishのテキスト表示
    private IEnumerator VisibleFinishText()
    {
        finishiText.text = "FINISH!";//Finishのテキスト表示
        //finishRenderer.enabled = true;//Finishの画像表示
        yield return new WaitForSeconds(3f);
        RoadResultScene();//リザルトシーンの遷移処理
    }



}
