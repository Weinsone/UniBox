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
    public static CameraController LocalPlayerCamera { get; private set; }

    private void Start() {
        LocalPlayer = new Player(Server.Clients.Count, "Local Player", Privileges.admin, ControllerList.Controllers.assistant);
        LocalPlayerCamera = new CameraController(GameObject.FindGameObjectWithTag("MainCamera"));

        PlayerMenu.CreatePlayerUI(GameObject.Find("MainCanvas"));

        if (Server.isHost) {
            // траханье с сокетами (мммм дельфи) (ахахах чую можно будет определить мой код по var'ам) (бля, разный почерк в коде, я такого еще не встречал)
            Server.Targets.Add(LocalPlayer.EntityGameObject.transform);
        }

        StartCoroutine(BotChecker());
        // PluginEngine.onProgramCompiled = new PluginEngine.CompiledPluginStateHandler(LocalPlayer.usingComputer.AddProgram);
    }

    private void Update() {
        if (InputHandler.IsMovementKeyPressed) { // Эта проверка нужна чтоб каждый кадр лишний раз не вызывались методы класса LocalPlayer.Controller
            LocalPlayer.Controller.Goto(new Vector3(InputHandler.HorizontalKeyInput, 0, InputHandler.VerticalKeyInput), false); // 1 - нажата W, (-1) - нажата S, аналогично с A (1), D (-1)
        }
        if (InputHandler.JumpInput) {
            LocalPlayer.Controller.Jump();
        }
    }

    private void LateUpdate() {
        if (InputHandler.IsGameplayMenuKeyPressed) {
            PlayerMenu.ShowCursor();
            if (InputHandler.GameplayMenuState) {
                PlayerMenu.ShowGameplayMenu();
            } else if (InputHandler.QuckMenuState) {
                PlayerMenu.ShowQuickMenu();
            }
        } else {
            PlayerMenu.HideCursor();
            PlayerMenu.HideGameplayMenu();
            PlayerMenu.HideQuickMenu();
            LocalPlayerCamera.View(InputHandler.HorizontalMouseInput, InputHandler.VerticalMouseInput); // НАСТРОИТЬ SENSITIVITY!
        }
        LocalPlayerCamera.UpdatePosition(LocalPlayer.EntityGameObject.transform.position + LocalPlayer.Controller.EyeLevel);
    }

    private IEnumerator BotChecker() {
        while (GameState.aiEnabled) {
            foreach (var bot in Server.Bots) {
                bot.Behavior.Checkup();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}