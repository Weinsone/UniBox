using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour { // ща по жопе получишь. ой извените
    public static Client LocalPlayer { get; private set; }
    public static string PidoraTest { get; private set; } = "vy gej";
    public static bool multiplayerMode;
    InputHandler handler = new InputHandler();

    void Start() {
        LocalPlayer = new Client(Server.clients.Count, "Player", Privileges.admin);
        handler.Initialize(LocalPlayer);
        if (multiplayerMode) {
            // траханье с сокетами (мммм дельфи) (ахахах чую можно будет определить мой код по var'ам)
        }
    }

    void Update() {
        handler.ReadInput();
    }

    void LateUpdate() {
        handler.ReadViewInput();
    }
}