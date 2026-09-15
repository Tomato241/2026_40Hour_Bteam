using UnityEngine;
using TMPro; //TextMeshProを使うために必要 
using UnityEngine.SceneManagement;//シーン管理に必要
using System.Collections;
using UnityEngine.InputSystem;


public class RaceLap_Manager : MonoBehaviour
{
    public int lapCount_1p = 0;//1Pのラップ数カウント用変数
    public int lapCount_2p = 0;//2Pのラップ数カウント用変数
    public int lapCount_3p = 0;//3Pのラップ数カウント用変数
    public int lapCount_4p = 0;//4Pのラップ数カウント用変数

    [Header("1Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_1P;//1Pのラップ数表示用変数
    [Header("2Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_2P;//2Pのラップ数表示用変数
    [Header("3Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_3P;//3Pのラップ数表示用変数
    [Header("4Pのラップ数のテキスト")][SerializeField] private TextMeshProUGUI lapCountText_4P;//4Pのラップ数表示用変数

    [Header("最大ラップ数")][SerializeField] private int maxRap;


    [Header("Finishのテキスト")][SerializeField] private TextMeshProUGUI finishiText; //Finishの表示用変数


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lapCount_1p = 1;
        lapCount_2p = 1;
        //lapCount_3p = 1;
        //lapCount_4p = 1;
        lapCountText_1P.text = lapCount_1p.ToString();
        lapCountText_2P.text = lapCount_2p.ToString();
        //lapCountText_3P.text = lapCount_3p.ToString();
        //lapCountText_4P.text = lapCount_4p.ToString();
        finishiText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.enterKey.wasPressedThisFrame)
        {
            ++lapCount_1p;
        }

        lapCountText_1P.text = lapCount_1p.ToString();
        lapCountText_2P.text = lapCount_2p.ToString();

        //Player1P～Player4P誰かがラップ数が最大ラップ数を超えたら
        if (lapCount_1p >= maxRap
            ||lapCount_2p >= maxRap)
        {
            StartCoroutine(VisibleFinishText());
            RoadResultScene();//リザルトシーンの
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            ++lapCount_1p;
        }
        if (collision.gameObject.CompareTag("Player2"))
        {
            ++lapCount_2p;
        }
        if (collision.gameObject.CompareTag("Player3"))
        {
            ++lapCount_3p;
        }
        if (collision.gameObject.CompareTag("Player4"))
        {
            ++lapCount_4p;
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
        yield return new WaitForSeconds(3f);
        RoadResultScene();//リザルトシーンの遷移処理
    }



}
