using UnityEngine;
using UnityEngine.SceneManagement;//シーン管理に必要
using System.Collections;
using UnityEngine.InputSystem;

public class Result_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartPlayresultBGM());
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current.bButton.wasPressedThisFrame)
        {
            StartCoroutine(RoadTitleScene());
        }
        else if(Gamepad.current.aButton.wasPressedThisFrame)
        {
            StartCoroutine(RoadRetry());
        }
    }

    private IEnumerator StartPlayresultBGM()
    {
        yield return new WaitForSeconds(0.1f);
        Sound_Manager.instance.PlayResultBGM();
    }


    private IEnumerator RoadTitleScene()
    {
        Player_Info.WinnerData = null;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Title");//リザルトシーンに遷移
    }

    private IEnumerator RoadRetry()
    {
        Player_Info.WinnerData = null;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game");//ゲームシーンに遷移（リトライ）
    }
}
