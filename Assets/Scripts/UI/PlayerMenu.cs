using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerMenu
{
    public static GameObject canvasObject;
    public static Canvas canvas;

    public static Form[] mainForms;
    public static Form[] quickForms;
    public static Vector3 CursorPosition {
        get {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    public static Ray CursorDirection {
        get {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

    private static bool isGameplayMenuEnabled, isQuickMenuEnabled;
    
    public static bool Visible {
        set {
            canvasObject.SetActive(value);
        }
    }

    public static void ShowCursor() {
        Cursor.visible = true;
    }

    public static void HideCursor() {
        if (!GameLevel.LocalPlayer.isUseComputer) {
            Cursor.visible = false;
        }
    }

    public static void CreatePlayerUI(GameObject foundObject) {
        canvasObject = foundObject;
        canvas = canvasObject.GetComponent<Canvas>();

        mainForms = GameMode.CreatePlayerMenu();
        // quickForms = GameMode.CreatePlayerQuickMenu();
    }

    public static void ShowContextMenu() {

    }

    public static void ShowGameplayMenu() {
        foreach (var mainForm in mainForms) {
            isGameplayMenuEnabled = true;
            mainForm.gameObject.SetActive(true);
        }
    }

    public static void HideGameplayMenu() {
        if (isGameplayMenuEnabled) {
            foreach (var mainForm in mainForms) {
                isGameplayMenuEnabled = false;
                mainForm.gameObject.SetActive(false);
            }
        }
    }

    public static void ShowQuickMenu() {
        // foreach (var quickForm in quickForms) {
        //     isQuickMenuEnabled = true;
        //     quickForm.gameObject.SetActive(true);
        // }

        bool leftMouseKeyState = InputHandler.IsLeftMouseKeyPressed;

        if (leftMouseKeyState) {
            if (FindComputer()) {
                return;
            }
        }

        ObjectTools.Implement(leftMouseKeyState);
    }

    public static void HideQuickMenu() {
        // if (isQuickMenuEnabled) {
        //     foreach (var quickForm in quickForms) {
        //         isQuickMenuEnabled = false;
        //         quickForm.gameObject.SetActive(false);
        //     }
        // }
    }

    public static void DrawPoints(Vector3[] points) {

    }

    public static void RemovePoints() {
        
    }

    private static bool FindComputer() {
        GameObject foundObject = Raycast.GetUIHit(Input.mousePosition);
        if (foundObject != null) {
            GameLevel.LocalPlayer.isUseComputer = true;
            GameLevel.LocalPlayer.usingComputer = foundObject.GetComponentInParent<VirtualMachine>();
            PluginEngine.onProgramCompiled = GameLevel.LocalPlayer.usingComputer.AddProgram;
            GameLevel.onExit = ExitComputer;
            return true;
        } else {
            return false;
        }
    }

    private static void ExitComputer() {
        GameLevel.LocalPlayer.isUseComputer = false;
        GameLevel.LocalPlayer.usingComputer = null;
    }
}
