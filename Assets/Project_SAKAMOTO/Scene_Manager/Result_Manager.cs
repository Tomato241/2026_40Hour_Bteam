using UnityEngine;
using UnityEngine.SceneManagement;//シーン管理に必要
using System.Collections;
using UnityEngine.InputSystem;

public class Result_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current.aButton.wasPressedThisFrame)
        {
            StartCoroutine(RoadTitleScene());
        }
        else if(Gamepad.current.bButton.wasPressedThisFrame)
        {
            StartCoroutine(RoadRetry());
        }
    }


    private IEnumerator RoadTitleScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Title");//リザルトシーンに遷移
    }

    private IEnumerator RoadRetry()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game");//ゲームシーンに遷移（リトライ）
    }
}
