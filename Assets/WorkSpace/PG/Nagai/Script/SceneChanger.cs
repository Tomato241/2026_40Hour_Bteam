using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//シーン遷移を行うクラス
public class SceneChanger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Gamepad.current != null) 
        {
            //今いるシーンがTitleSceneならMainSceneに遷移する
            if (SceneManager.GetActiveScene().name == "TitleScene" && Gamepad.current.aButton.wasPressedThisFrame)
            {
                SceneManager.LoadScene("MainScene");
            }

            //今いるシーンがMainSceneならResultSceneに遷移する
            if (SceneManager.GetActiveScene().name == "MainScene" && Gamepad.current.bButton.wasPressedThisFrame)
            {
                SceneManager.LoadScene("ResultScene");
            }

            //今いるシーンがResultSceneならTitleSceneに遷移する
            if (SceneManager.GetActiveScene().name == "ResultScene" && Gamepad.current.aButton.wasPressedThisFrame)
            {
                SceneManager.LoadScene("MainScene");
            }
            else if (SceneManager.GetActiveScene().name == "ResultScene" && Gamepad.current.bButton.wasPressedThisFrame)
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
}
