using UnityEngine;
using UnityEngine.SceneManagement;//シーン管理に必要
using System.Collections;
using UnityEngine.InputSystem;

public class Result_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sound_Manager.instance.PlayResultBGM();
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


    private IEnumerator RoadTitleScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TitleS");//リザルトシーンに遷移
    }

    private IEnumerator RoadRetry()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameS");//ゲームシーンに遷移（リトライ）
    }
}
