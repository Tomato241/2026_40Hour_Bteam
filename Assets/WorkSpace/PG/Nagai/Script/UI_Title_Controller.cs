using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Title_Controller : MonoBehaviour
{
    [SerializeField] private Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(OnStartPressed);
    }

    void OnStartPressed()
    {
        SceneManager.LoadScene("MainScene");
    }
}