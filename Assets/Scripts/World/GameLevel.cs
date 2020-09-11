using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Тупо батя: отвечает игровой цикл
    TODO: Нужно будет переименовать класс, т.к его роль с контроллера уровня изменилась на управление игровым циклом (LocalServer? GameCycle?) да не и так норм
*/
public class GameLevel : MonoBehaviour
{
    public static Player LocalPlayer { get; private set; } // Локальный игрок и его тушка (LocalPlayer.Model, LocalPlayer.Controller)
    public static CameraController LocalPlayerCamera { get; private set; } // прекол, но судя по коду - камера отдельная сущность, которая к игроку не имеет отношения. хз, хорошо это или плохо. если камера будет отлетать от модели игрока, то все норм, даже неплохо

    private void Start() {
        LocalPlayer = new Player(Server.Clients.Count, "Local Player", Privileges.admin, ControllerList.Controllers.mainPlayer);
        LocalPlayerCamera = new CameraController(GameObject.FindGameObjectWithTag("MainCamera"));
        if (Server.isHost) {
            // траханье с сокетами (мммм дельфи) (ахахах чую можно будет определить мой код по var'ам) (бля, разный почерк в коде, я такого еще не встречал)
            Server.Targets.Add(LocalPlayer.EntityModel.transform);
        }
    }

    private void Update() {
        if (InputHandler.IsMovementKeyPressed) { // Эта проверка нужна чтоб каждый кадр лишний раз не вызывались методы класса LocalPlayer.Controller
            LocalPlayer.Controller.Move(InputHandler.HorizontalKeyInput, InputHandler.VerticalKeyInput); // 1 - нажата W, (-1) - нажата S, аналогично с A (1), D (-1)
        }
        if (InputHandler.JumpInput) {
            LocalPlayer.Controller.Jump();
        }
        LocalPlayer.Animator.Animate(InputHandler.HorizontalKeyInput, InputHandler.VerticalKeyInput);
    }

    private void LateUpdate() {
        if (InputHandler.IsCursorShowKeyPressed) {
            PlayerMenu.ShowCursor();
        } else {
            PlayerMenu.HideCursor();
            LocalPlayerCamera.View(InputHandler.HorizontalMouseInput, InputHandler.VerticalMouseInput); // НАСТРОИТЬ SENSITIVITY!
        }
        LocalPlayerCamera.UpdatePosition(LocalPlayer.Controller.transform.position + LocalPlayer.Controller.eyeLevel);
    }
}