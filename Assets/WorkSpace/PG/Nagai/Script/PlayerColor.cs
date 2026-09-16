using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    public int playerId; // 1 or 2 などをインスペクターで設定

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();

        if (playerId == 1)
        {
            rend.material.color = Color.red;
        }
        else if (playerId == 2)
        {
            rend.material.color = Color.blue;
        }
    }
}