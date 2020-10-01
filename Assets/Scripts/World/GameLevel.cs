using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    Тупо батя: отвечает игровой цикл
*/
public class GameLevel : MonoBehaviour
{
    public static Player LocalPlayer { get; private set; } // Представление локального игрока и его тушка (LocalPlayer.Model, LocalPlayer.Controller)
    public static CameraController LocalPlayerCamera { get; private set; }

    public delegate void ExitButtonStateHandler();
    public static ExitButtonStateHandler onExit;

    private void Start() {
        LocalPlayer = new Player(Server.Clients.Count, "Local Player", Privileges.admin, ControllerList.Controllers.assistant, new Vector3(0, 0.5f, 0));
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
            LocalPlayer.MoveTowards(new Vector3(InputHandler.HorizontalKeyInput, 0, InputHandler.VerticalKeyInput)); // 1 - нажата W, (-1) - нажата S, аналогично с A (1), D (-1)
        } else {
            LocalPlayer.Stop();
        }
        if (InputHandler.JumpInput) {
            LocalPlayer.Jump();
        }
        if (InputHandler.IsExitButtonPressed) {
            onExit();
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
            LocalPlayerCamera.UpdateViewDirection(InputHandler.HorizontalMouseInput, InputHandler.VerticalMouseInput); // НАСТРОИТЬ SENSITIVITY!
        }
        LocalPlayerCamera.UpdatePosition(LocalPlayer.EntityGameObject.transform.position + LocalPlayer.EyeLevel);
    }

    private IEnumerator BotChecker() {
        while (GameState.aiEnabled) {
            foreach (var bot in Server.Bots) {
                bot.Behavior.Checkup();
            }
            // yield return new WaitForSeconds(0.1f);
            yield return null;
        }
    }
}