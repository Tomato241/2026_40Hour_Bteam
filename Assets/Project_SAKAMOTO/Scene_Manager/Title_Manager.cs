using UnityEngine;
using UnityEngine.SceneManagement;//シーン管理に必要
using System.Collections;
using UnityEngine.InputSystem;

public class Title_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sound_Manager.instance.PlayTitleBGM();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.aButton.wasPressedThisFrame)
        {
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        Sound_Manager.instance.GameStartButtonSE();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game");//ゲームシーンに遷移（リトライ）
    }
}
