using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Тупо батя: отвечает игровой цикл
*/
public class GameLevel : MonoBehaviour
{
    public static Client LocalPlayer { get; private set; } // Локальный игрок и его тушка
    public static CameraController PlayerCamera { get; private set; } // прекол, но судя по кода - камера отдельная сущность, которая к игроку не имеет отношения. хз, хорошо это или плохо. если камера будет отлетать от тушки игрока, то все норм
    public static bool isMultiplayerMode; // нужно перенести в класс Server

    void Start() {
        LocalPlayer = new Client(Server.clients.Count, "Local Player", Privileges.admin);
        PlayerCamera = new CameraController(GameObject.FindGameObjectWithTag("MainCamera"));
        if (isMultiplayerMode) {
            // траханье с сокетами (мммм дельфи) (ахахах чую можно будет определить мой код по var'ам)
        }
    }

    void Update() {
        LocalPlayer.Controller.Move(InputHandler.HorizontalKeyInput, 0, InputHandler.VerticalKeyInput); // 1 - нажата W, -1 - нажата S, аналогично с A, D

        if (InputHandler.IsJumpKeyPressed()) {
            LocalPlayer.Controller.Jump();
        }
    }

    void LateUpdate() {
        PlayerCamera.View(InputHandler.HorizontalMouseInput, InputHandler.VerticalMouseInput); // НАСТРОИТЬ SENSITIVITY!
        PlayerCamera.UpdatePosition(LocalPlayer.Controller.transform.position);
    }
}