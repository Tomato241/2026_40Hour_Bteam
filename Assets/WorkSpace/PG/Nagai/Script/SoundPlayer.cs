using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private Sound_Manager sound_Manager;
    void Start()
    {
        sound_Manager.PlayTitleBGM();
    }
    void Update()
    {
        
    }
}
