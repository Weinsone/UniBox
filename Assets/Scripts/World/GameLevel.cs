using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public static Client LocalPlayer { get; private set; }
    public static CameraController PlayerCamera { get; private set; }
    public static bool isMultiplayerMode;

    void Start() {
        LocalPlayer = new Client(Server.clients.Count, "Player", Privileges.admin);
        PlayerCamera = new CameraController(GameObject.FindGameObjectWithTag("MainCamera"));
        InputHandler.Initialize(LocalPlayer, PlayerCamera);
        if (isMultiplayerMode) {
            // траханье с сокетами (мммм дельфи) (ахахах чую можно будет определить мой код по var'ам)
        }
    }

    void Update() {
        InputHandler.ReadKeyInput();
    }

    void LateUpdate() {
        InputHandler.ReadMouseInput();
    }
}