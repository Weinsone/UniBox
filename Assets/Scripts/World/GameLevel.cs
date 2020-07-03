using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Тупо батя: отвечает игровой цикл
    Нужно будет переименовать класс, т.к его роль с контроллера уровня изменилась на управление игровым циклом (LocalServer? GameCycle?)
*/
public class GameLevel : MonoBehaviour
{
    public static Player LocalPlayer { get; private set; } // Локальный игрок и его тушка
    public static CameraController PlayerCamera { get; private set; } // прекол, но судя по коду - камера отдельная сущность, которая к игроку не имеет отношения. хз, хорошо это или плохо. если камера будет отлетать от модели игрока, то все норм, даже неплохо
    public static bool isMultiplayerMode; // нужно перенести в класс Server, типа Server.IsEnabled

    private void Start() {
        LocalPlayer = new Player(Server.Clients.Count, "Local Player", Privileges.admin);
        PlayerCamera = new CameraController(GameObject.FindGameObjectWithTag("MainCamera"));
        if (isMultiplayerMode) {
            // траханье с сокетами (мммм дельфи) (ахахах чую можно будет определить мой код по var'ам) (бля, разный почерк в коде, я такого еще не встречал)
        }
    }

    private void Update() {
        if (InputHandler.IsMovementKeyPressed) { // Эта проверка нужна чтоб каждый кадр лишний раз не вызывались методы класса LocalPlayer.Controller
            LocalPlayer.Controller.Move(InputHandler.HorizontalKeyInput, InputHandler.VerticalKeyInput); // 1 - нажата W, (-1) - нажата S, аналогично с A, D
        }
        if (InputHandler.JumpInput) {
            LocalPlayer.Controller.Jump();
        }
    }

    private void LateUpdate() {
        PlayerCamera.View(InputHandler.HorizontalMouseInput, InputHandler.VerticalMouseInput); // НАСТРОИТЬ SENSITIVITY!
        PlayerCamera.UpdatePosition(LocalPlayer.Controller.transform.position);
    }

    private void FixedUpdate() {
        // чё-то типа Server.UpdatePosition(LocalPlayer.Controller.transform.position);
    }
}