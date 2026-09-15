using UnityEngine;
using UnityEngine.InputSystem;

//複数のコントローラーの接続状態を管理し、プレイヤーに割り当てるスクリプト
public class ControllerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMove[] players; //シーン上の各プレイヤーオブジェクトをインスペクターで登録

    private Gamepad[] controllers = new Gamepad[4];

    void Update()
    {
        for (int i = 0; i < controllers.Length && i < players.Length; i++)
        {
            Gamepad current = Gamepad.all.Count > i ? Gamepad.all[i] : null;

            if (controllers[i] == current) continue; //変化なし

            controllers[i] = current;
            players[i].AssignGamepad(current);

            Debug.Log(current != null
                ? $"Controller {i + 1} connected: {current.displayName}"
                : $"Controller {i + 1} disconnected.");
        }
    }
}